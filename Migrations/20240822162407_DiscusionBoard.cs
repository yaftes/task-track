using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class DiscusionBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_DiscussionBoard_DiscussionBoardId",
                table: "Message");

            migrationBuilder.DropTable(
                name: "DiscussionBoard");

            migrationBuilder.DropColumn(
                name: "DiscussionBoardId",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "DiscussionBoardId",
                table: "Message",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_DiscussionBoardId",
                table: "Message",
                newName: "IX_Message_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Project_ProjectId",
                table: "Message",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Project_ProjectId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Message",
                newName: "DiscussionBoardId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ProjectId",
                table: "Message",
                newName: "IX_Message_DiscussionBoardId");

            migrationBuilder.AddColumn<int>(
                name: "DiscussionBoardId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DiscussionBoard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionBoard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionBoard_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionBoard_ProjectId",
                table: "DiscussionBoard",
                column: "ProjectId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_DiscussionBoard_DiscussionBoardId",
                table: "Message",
                column: "DiscussionBoardId",
                principalTable: "DiscussionBoard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
