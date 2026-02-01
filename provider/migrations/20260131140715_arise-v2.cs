using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace arise_api.provider.migrations
{
    /// <inheritdoc />
    public partial class arisev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "usr",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "usr",
                table: "users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "usr",
                table: "users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "usr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "usr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "usr",
                table: "users");
        }
    }
}
