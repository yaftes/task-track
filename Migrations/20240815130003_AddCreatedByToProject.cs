using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Skill_SkillId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SkillId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SkillId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Created_By",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicationUserSkill",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSkill", x => new { x.ApplicationUserId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSkill_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSkill_SkillId",
                table: "ApplicationUserSkill",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserSkill");

            migrationBuilder.DropColumn(
                name: "Created_By",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "SkillId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SkillId",
                table: "AspNetUsers",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Skill_SkillId",
                table: "AspNetUsers",
                column: "SkillId",
                principalTable: "Skill",
                principalColumn: "Id");
        }
    }
}
