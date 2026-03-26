using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace arise_api.provider.migrations
{
    /// <inheritdoc />
    public partial class arisev5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_employeeStatuses_StatusId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_users_UserId",
                schema: "emp",
                table: "employees");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "emp",
                table: "employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "departments",
                schema: "emp",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_DepartmentId",
                schema: "emp",
                table: "employees",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DepartmentId",
                schema: "emp",
                table: "employees",
                column: "DepartmentId",
                principalSchema: "emp",
                principalTable: "departments",
                principalColumn: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_employeeStatuses_StatusId",
                schema: "emp",
                table: "employees",
                column: "StatusId",
                principalSchema: "emp",
                principalTable: "employeeStatuses",
                principalColumn: "EmployeeStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_employees_users_UserId",
                schema: "emp",
                table: "employees",
                column: "UserId",
                principalSchema: "usr",
                principalTable: "users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DepartmentId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_employeeStatuses_StatusId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_users_UserId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropTable(
                name: "departments",
                schema: "emp");

            migrationBuilder.DropIndex(
                name: "IX_employees_DepartmentId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "emp",
                table: "employees");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_employeeStatuses_StatusId",
                schema: "emp",
                table: "employees",
                column: "StatusId",
                principalSchema: "emp",
                principalTable: "employeeStatuses",
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
    }
}
