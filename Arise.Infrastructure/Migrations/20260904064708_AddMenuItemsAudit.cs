using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuItemsAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "mnu",
                table: "menuItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "mnu",
                table: "menuItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "mnu",
                table: "menuItems",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "mnu",
                table: "menuItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "mnu",
                table: "menuItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "mnu",
                table: "menuItems");
        }
    }
}
