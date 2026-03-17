using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace arise_api.provider.migrations
{
    /// <inheritdoc />
    public partial class arisev4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_users_UserId",
                schema: "emp",
                table: "employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "emp",
                table: "employees",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "emp",
                table: "employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dni",
                schema: "emp",
                table: "employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "emp",
                table: "employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "emp",
                table: "employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                schema: "emp",
                table: "employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "employee_statuses",
                schema: "emp",
                columns: table => new
                {
                    EmployeeStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_statuses", x => x.EmployeeStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_StatusId",
                schema: "emp",
                table: "employees",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_employee_statuses_StatusId",
                schema: "emp",
                table: "employees",
                column: "StatusId",
                principalSchema: "emp",
                principalTable: "employee_statuses",
                principalColumn: "EmployeeStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_users_UserId",
                schema: "emp",
                table: "employees",
                column: "UserId",
                principalSchema: "usr",
                principalTable: "users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_employee_statuses_StatusId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_users_UserId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropTable(
                name: "employee_statuses",
                schema: "emp");

            migrationBuilder.DropIndex(
                name: "IX_employees_StatusId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Dni",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "StatusId",
                schema: "emp",
                table: "employees");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "emp",
                table: "employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_users_UserId",
                schema: "emp",
                table: "employees",
                column: "UserId",
                principalSchema: "usr",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
