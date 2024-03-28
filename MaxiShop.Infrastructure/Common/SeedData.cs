using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.Dbcontexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Common
{
    public class SeedData
    {
        public static async Task SeedRoles(IServiceProvider ServiceProvider)
        {
            using var scope= ServiceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new List<IdentityRole>
            {
                new IdentityRole{Name="ADMIN",NormalizedName="ADMIN"},
                 new IdentityRole{Name="CUSTOMER",NormalizedName="CUSTOMER"}
            };
            foreach (var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
        public static async Task SeedDataAsync(MaxiShopDbContext _dbContext)
        {
            if (!_dbContext.Brand.Any())
            {
                  _dbContext.AddRange(
                    new Brand
                    {
                        Name = "Apple",
                        EstablishedYear = 1989
                    },
                     new Brand
                     {
                         Name = "Oppo",
                         EstablishedYear = 1980
                     },
                      new Brand
                      {
                          Name = "One plus",
                          EstablishedYear = 2000
                      },
                       new Brand
                       {
                           Name = "Vivo",
                           EstablishedYear = 1999
                       },
                        new Brand
                        {
                            Name = "LG",
                            EstablishedYear = 1980
                        },
                         new Brand
                         {
                             Name = "Samsung",
                             EstablishedYear = 1997
                         });
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
