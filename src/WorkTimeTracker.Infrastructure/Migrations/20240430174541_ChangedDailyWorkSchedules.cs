using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTimeTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDailyWorkSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakTimeNorm",
                table: "DailyWorkSchedules");

            migrationBuilder.DropColumn(
                name: "NightOvertime",
                table: "DailyWorkSchedules");

            migrationBuilder.DropColumn(
                name: "NightWorkHours",
                table: "DailyWorkSchedules");

            migrationBuilder.RenameColumn(
                name: "WorkTimeNorm",
                table: "DailyWorkSchedules",
                newName: "RealWorkTime");

            migrationBuilder.RenameColumn(
                name: "WorkHours",
                table: "DailyWorkSchedules",
                newName: "RealBreakTime");

            migrationBuilder.RenameColumn(
                name: "OvertimeCollected",
                table: "DailyWorkSchedules",
                newName: "PlannedWorkTime");

            migrationBuilder.RenameColumn(
                name: "Overtime",
                table: "DailyWorkSchedules",
                newName: "PlannedBreakTime");

            migrationBuilder.AddColumn<double>(
                name: "RealOvertimeMinutes",
                table: "DailyWorkSchedules",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealOvertimeMinutes",
                table: "DailyWorkSchedules");

            migrationBuilder.RenameColumn(
                name: "RealWorkTime",
                table: "DailyWorkSchedules",
                newName: "WorkTimeNorm");

            migrationBuilder.RenameColumn(
                name: "RealBreakTime",
                table: "DailyWorkSchedules",
                newName: "WorkHours");

            migrationBuilder.RenameColumn(
                name: "PlannedWorkTime",
                table: "DailyWorkSchedules",
                newName: "OvertimeCollected");

            migrationBuilder.RenameColumn(
                name: "PlannedBreakTime",
                table: "DailyWorkSchedules",
                newName: "Overtime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "BreakTimeNorm",
                table: "DailyWorkSchedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "NightOvertime",
                table: "DailyWorkSchedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "NightWorkHours",
                table: "DailyWorkSchedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
