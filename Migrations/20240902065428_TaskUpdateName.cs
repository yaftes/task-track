using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class TaskUpdateName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubTaskStatus_SubTaskId",
                table: "SubTaskStatus");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Task",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "SubTaskFile",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SubTask",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubTaskStatus_SubTaskId",
                table: "SubTaskStatus",
                column: "SubTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubTaskStatus_SubTaskId",
                table: "SubTaskStatus");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "SubTaskFile");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SubTask");

            migrationBuilder.CreateIndex(
                name: "IX_SubTaskStatus_SubTaskId",
                table: "SubTaskStatus",
                column: "SubTaskId",
                unique: true);
        }
    }
}
