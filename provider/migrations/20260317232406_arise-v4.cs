using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                name: "employeeStatuses",
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
                    table.PrimaryKey("PK_employeeStatuses", x => x.EmployeeStatusId);
                });

            migrationBuilder.InsertData(
                schema: "emp",
                table: "employeeStatuses",
                columns: new[] { "EmployeeStatusId", "Code", "Color", "Name" },
                values: new object[,]
                {
                    { new Guid("7d2e9f1a-3c4b-4e8f-b2a5-6d1c0e7f8a9b"), "INA", "#DC3545", "Inactive" },
                    { new Guid("a5b8c3e1-6f2d-4a9e-c7b4-1e3d5f2a8c6b"), "PEN", "#FFC107", "Pending" },
                    { new Guid("f3a1c2d4-8b5e-4f7a-9c6d-2e1b0a3f4e5d"), "ACT", "#28A745", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_StatusId",
                schema: "emp",
                table: "employees",
                column: "StatusId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_employeeStatuses_StatusId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropForeignKey(
                name: "FK_employees_users_UserId",
                schema: "emp",
                table: "employees");

            migrationBuilder.DropTable(
                name: "employeeStatuses",
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
