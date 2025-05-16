using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1112 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccumulatedVacationDays",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccumulatedVacationDays",
                table: "Employees");
        }
    }
}
