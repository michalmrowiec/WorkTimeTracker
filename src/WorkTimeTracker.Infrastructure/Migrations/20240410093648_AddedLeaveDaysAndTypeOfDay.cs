using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTimeTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLeaveDaysAndTypeOfDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeOfDay",
                table: "DailyWorkSchedules",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VacationDays",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfDay",
                table: "DailyWorkSchedules");

            migrationBuilder.DropColumn(
                name: "VacationDays",
                table: "AspNetUsers");
        }
    }
}
