using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class ProgressAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Progress",
                table: "Task",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Progress",
                table: "Project",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Project");
        }
    }
}
