using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_SEM2_HNDShop.Migrations
{
    public partial class App1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(nullable: true),
                    BrandDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CateType = table.Column<string>(nullable: true),
                    CateDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoId = table.Column<int>(nullable: false),
                    SubBrandId = table.Column<int>(nullable: false),
                    CateId = table.Column<int>(nullable: false),
                    ProCode = table.Column<string>(nullable: true),
                    ProName = table.Column<string>(nullable: true),
                    ProImage = table.Column<string>(nullable: true),
                    ProDesc = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    OriginPrice = table.Column<double>(nullable: false),
                    SellPrice = table.Column<double>(nullable: false),
                    StatusCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromoTitle = table.Column<string>(nullable: true),
                    DiscountPercent = table.Column<int>(nullable: false),
                    PromoDesc = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PromoStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Promotions");
        }
    }
}
