using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class Addd1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAccumulatedVacationDays",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAccumulatedVacationDays",
                table: "Employees");
        }
    }
}
