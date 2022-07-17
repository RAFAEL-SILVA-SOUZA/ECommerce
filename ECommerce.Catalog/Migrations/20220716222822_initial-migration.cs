using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Catalog.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("144267e5-2051-4c3a-a0a9-01b2ee5fe56c")),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("011d281a-3743-4419-b218-1da1ec244928"), "Teclado", 190.015841579443160m, 10 },
                    { new Guid("06804894-134e-457b-bb2f-c5b67a406ead"), "Peixe", 392.148567140259155m, 10 },
                    { new Guid("3af74a2c-de1c-4deb-bc59-bb54a2544f92"), "Bola", 378.265193931549425m, 10 },
                    { new Guid("9b3d9816-a6cf-47f2-bd4f-16a823adec8c"), "Frango", 111.082403591643380m, 10 },
                    { new Guid("b45e6e1a-d75f-4cac-bda5-c60e6fa6a4c8"), "Peixe", 497.537901027077180m, 10 },
                    { new Guid("bce1ecc0-ea8e-43a5-8347-691eb973b304"), "Frango", 321.097393587491285m, 10 },
                    { new Guid("cb84eaf1-d048-4a9f-b7c5-f69d23a46a75"), "Frango", 210.498992430441215m, 10 },
                    { new Guid("d0b35cc4-f48f-4354-9539-eca93a13a8d7"), "Cadeira", 498.064014599522630m, 10 },
                    { new Guid("d121361b-1d7c-4415-b494-009ee496c205"), "Atum", 429.242828059365260m, 10 },
                    { new Guid("f5649983-33c1-4a4d-9930-e24b651e91db"), "Luvas", 219.134668027185455m, 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
