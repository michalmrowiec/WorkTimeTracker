using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTimeTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorkTimeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BadgeId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfEmployment",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pesel",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportsToId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Workload",
                table: "AspNetUsers",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DailyWorkSchedules",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedWorkStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlannedWorkEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkTimeNorm = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakTimeNorm = table.Column<TimeSpan>(type: "time", nullable: false),
                    RealWorkStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RealWorkEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkHours = table.Column<TimeSpan>(type: "time", nullable: false),
                    NightWorkHours = table.Column<TimeSpan>(type: "time", nullable: false),
                    Overrime = table.Column<TimeSpan>(type: "time", nullable: false),
                    NightOvertime = table.Column<TimeSpan>(type: "time", nullable: false),
                    OvertimeCollected = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyWorkSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyWorkSchedules_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionTimes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOfAction = table.Column<TimeSpan>(type: "time", nullable: false),
                    DailyWorkScheduleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionTimes_DailyWorkSchedules_DailyWorkScheduleId",
                        column: x => x.DailyWorkScheduleId,
                        principalTable: "DailyWorkSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionTimes_DailyWorkScheduleId",
                table: "ActionTimes",
                column: "DailyWorkScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWorkSchedules_EmployeeId",
                table: "DailyWorkSchedules",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionTimes");

            migrationBuilder.DropTable(
                name: "DailyWorkSchedules");

            migrationBuilder.DropColumn(
                name: "BadgeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfEmployment",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pesel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReportsToId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Workload",
                table: "AspNetUsers");
        }
    }
}
