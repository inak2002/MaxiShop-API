using MaxiShop.Infrastructure.Dbcontexts;
using Microsoft.EntityFrameworkCore;
using MaxiShop.Infrastructure;
using MaxiShop.Application;
using Microsoft.OpenApi.Writers;
using MaxiShop.Infrastructure.Common;
using MaxiShop.Web.Middlewares;
using Microsoft.AspNetCore.Identity;
using MaxiShop.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddCors(options=> { options.AddPolicy("CustomPolicy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); });

builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default", new CacheProfile
    {
        Duration = 0
    });
});
builder.Host.UseSerilog((Context, Config) =>
{
    Config.WriteTo.File("Logs/Log.txt", rollingInterval:RollingInterval.Day);
    if (Context.HostingEnvironment.IsProduction() == false)
    {
        Config.WriteTo.Console();
    }
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            Description = @"Jwt Authorization header using the Bearer Scheme.
               Enter 'Bearer[space]and then your token' in the input below.
               Example: Bearer 12345abcdef",
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()

        }
    });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MaxiShop API Version 1",
        Description = "Developed by kani",
        Version = "v1.0",
    });
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "MaxiShop API Version 2",
        Description = "Developed by kani",
        Version = "v2.0",
    });
});
#region Configuration for seeding data to database
static async void updateDatabaseAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<MaxiShopDbContext>();
            if (context.Database.IsSqlServer())
            {
                context.Database.Migrate();

            }
            await SeedData.SeedDataAsync(context);
        }
        catch(Exception ex) 
        { 
            var logger =scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occured while migrating or seeding database");
        }
    }
   
}

#endregion
builder.Services.AddResponseCaching();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services
    .AddDbContext<MaxiShopDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
{
    options.SignIn.RequireConfirmedEmail = false;
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<MaxiShopDbContext>();
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
updateDatabaseAsync(app);

var serviceProvider = app.Services;
await SeedData.SeedRoles(serviceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/Swagger/v1/Swagger.json", "MaxiShop_v1");
        options.SwaggerEndpoint("/Swagger/v2/Swagger.json", "MaxiShop_v2");
    });
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/Swagger/v1/Swagger.json", "MaxiShop_v1");
    options.SwaggerEndpoint("/Swagger/v2/Swagger.json", "MaxiShop_v2");
    options.RoutePrefix = string.Empty;
});
app.UseCors("CustomPolicy");

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
