using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkTimeTracker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedActionTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ActionTimes");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "ActionTimes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsWork",
                table: "ActionTimes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ActionTimes_EmployeeId",
                table: "ActionTimes",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActionTimes_AspNetUsers_EmployeeId",
                table: "ActionTimes",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActionTimes_AspNetUsers_EmployeeId",
                table: "ActionTimes");

            migrationBuilder.DropIndex(
                name: "IX_ActionTimes_EmployeeId",
                table: "ActionTimes");

            migrationBuilder.DropColumn(
                name: "IsWork",
                table: "ActionTimes");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "ActionTimes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ActionTimes",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");
        }
    }
}
