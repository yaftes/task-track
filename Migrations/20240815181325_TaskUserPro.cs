using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class TaskUserPro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Assigned_to",
                table: "Task",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_Assigned_to",
                table: "Task",
                column: "Assigned_to");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_Assigned_to",
                table: "Task",
                column: "Assigned_to",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_Assigned_to",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_Assigned_to",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Assigned_to",
                table: "Task");
        }
    }
}
