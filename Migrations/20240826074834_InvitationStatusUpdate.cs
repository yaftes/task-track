using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class InvitationStatusUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Invitation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_TaskId",
                table: "Invitation",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Task_TaskId",
                table: "Invitation",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Task_TaskId",
                table: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Invitation_TaskId",
                table: "Invitation");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Invitation");
        }
    }
}
