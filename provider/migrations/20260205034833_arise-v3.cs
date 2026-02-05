using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace arise_api.provider.migrations
{
    /// <inheritdoc />
    public partial class arisev3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "emp");

            migrationBuilder.CreateTable(
                name: "employees",
                schema: "emp",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaternalLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaternalLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_employees_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "usr",
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_UserId",
                schema: "emp",
                table: "employees",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees",
                schema: "emp");
        }
    }
}
