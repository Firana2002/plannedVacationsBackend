using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIsManagerNotificationToNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsManagerNotification",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsManagerNotification",
                table: "Notifications");
        }
    }
}
