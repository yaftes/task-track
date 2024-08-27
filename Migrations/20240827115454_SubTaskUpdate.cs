using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class SubTaskUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTask_Task_ProjectId",
                table: "SubTask");

            migrationBuilder.DropColumn(
                name: "Assigned_to",
                table: "SubTask");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "SubTask",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_SubTask_ProjectId",
                table: "SubTask",
                newName: "IX_SubTask_TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTask_Task_TaskId",
                table: "SubTask",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTask_Task_TaskId",
                table: "SubTask");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "SubTask",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_SubTask_TaskId",
                table: "SubTask",
                newName: "IX_SubTask_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "Assigned_to",
                table: "SubTask",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubTask_Task_ProjectId",
                table: "SubTask",
                column: "ProjectId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
