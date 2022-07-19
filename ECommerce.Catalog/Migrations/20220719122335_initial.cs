using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Catalog.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("3e6af2f9-f863-4be7-bffc-e5955fe4c2d1")),
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
                    { new Guid("1a5249d1-c924-44ef-9212-e824cfa1d4c8"), "Calças", 112.94255232547160m, 10 },
                    { new Guid("1defafb2-69cc-46f0-9457-c78603bb4e1f"), "Salada", 408.710706872333630m, 10 },
                    { new Guid("30d7855b-3c53-4da0-be13-f6609f703a1e"), "Bicicleta", 495.481408533378680m, 10 },
                    { new Guid("57461727-c82d-4ed1-8e95-b9be7909214e"), "Atum", 182.907089399712695m, 10 },
                    { new Guid("8b3af8b5-b089-42d2-a587-23286d1e06b3"), "Frango", 419.089636952498705m, 10 },
                    { new Guid("aa6b6a42-2c0c-4b34-b2d3-256895465025"), "Frango", 175.0055013769145m, 10 },
                    { new Guid("b32cb73e-9b62-46af-93be-20f3540ba9f2"), "Carro", 117.829601270890790m, 10 },
                    { new Guid("ba2985d7-fda6-4bc2-b1e5-a61d2928f30d"), "Camiseta", 142.270791389331710m, 10 },
                    { new Guid("cb43d2e5-ddcf-4e88-8236-37b84f89c808"), "Salsicha", 296.857357674779435m, 10 },
                    { new Guid("d75999b8-7e48-4b62-adf9-99ac038b7802"), "Bola", 196.933722862309340m, 10 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
