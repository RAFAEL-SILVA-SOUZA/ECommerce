using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Order.Migrations
{
    public partial class initialaddcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("56d9ee5a-846f-4a0c-8c90-eebd97f4c610"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("d3120158-86ca-4971-9c18-34af6ff40532"));

            migrationBuilder.AddColumn<string>(
                name: "GatewayName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TranzactionId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("d1d9ac04-2509-4f93-b202-1da5cca6827a"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("b401dd66-d875-41b5-b33e-9ed7e87bada4"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GatewayName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TranzactionId",
                table: "Orders");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("d3120158-86ca-4971-9c18-34af6ff40532"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("56d9ee5a-846f-4a0c-8c90-eebd97f4c610"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("b401dd66-d875-41b5-b33e-9ed7e87bada4"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("d1d9ac04-2509-4f93-b202-1da5cca6827a"));
        }
    }
}
