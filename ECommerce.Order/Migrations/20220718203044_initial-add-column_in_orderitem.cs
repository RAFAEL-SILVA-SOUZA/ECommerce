using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Order.Migrations
{
    public partial class initialaddcolumn_in_orderitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("1c2af2ca-3e5f-4a94-be32-328e57e9de9e"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("56d9ee5a-846f-4a0c-8c90-eebd97f4c610"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("0ec92fea-33be-47fb-82ab-ee55ee9d5f84"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("d1d9ac04-2509-4f93-b202-1da5cca6827a"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("56d9ee5a-846f-4a0c-8c90-eebd97f4c610"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("1c2af2ca-3e5f-4a94-be32-328e57e9de9e"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("d1d9ac04-2509-4f93-b202-1da5cca6827a"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("0ec92fea-33be-47fb-82ab-ee55ee9d5f84"));
        }
    }
}
