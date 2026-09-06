using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arise.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menuItemRoles_Role_RoleId",
                schema: "mnu",
                table: "menuItemRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_Role_RoleId",
                schema: "usr",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "roles",
                newSchema: "usr");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                schema: "usr",
                table: "roles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_menuItemRoles_roles_RoleId",
                schema: "mnu",
                table: "menuItemRoles",
                column: "RoleId",
                principalSchema: "usr",
                principalTable: "roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_roles_RoleId",
                schema: "usr",
                table: "userRoles",
                column: "RoleId",
                principalSchema: "usr",
                principalTable: "roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_menuItemRoles_roles_RoleId",
                schema: "mnu",
                table: "menuItemRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userRoles_roles_RoleId",
                schema: "usr",
                table: "userRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                schema: "usr",
                table: "roles");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "usr",
                newName: "Role");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_menuItemRoles_Role_RoleId",
                schema: "mnu",
                table: "menuItemRoles",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userRoles_Role_RoleId",
                schema: "usr",
                table: "userRoles",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
