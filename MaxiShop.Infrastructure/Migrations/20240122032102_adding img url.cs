using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxiShop.Infrastructure.Migrations
{
    public partial class addingimgurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imageurl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imageurl",
                table: "Products");
        }
    }
}
