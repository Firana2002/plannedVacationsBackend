using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOnlySupport1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysCount",
                table: "EmployeeVacationDays");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "EmployeeVacationDays");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "EmployeeVacationDays",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "EmployeeVacationDays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "PastVacations",
                columns: table => new
                {
                    PastVacationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    VacationTypeId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastVacations", x => x.PastVacationId);
                    table.ForeignKey(
                        name: "FK_PastVacations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PastVacations_VacationTypes_VacationTypeId",
                        column: x => x.VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "VacationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PastVacations_EmployeeId",
                table: "PastVacations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PastVacations_VacationTypeId",
                table: "PastVacations",
                column: "VacationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PastVacations");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "EmployeeVacationDays");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "EmployeeVacationDays");

            migrationBuilder.AddColumn<int>(
                name: "DaysCount",
                table: "EmployeeVacationDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "EmployeeVacationDays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
