using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class MessageFirstAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created_On",
                table: "Message",
                newName: "Created_At");

            migrationBuilder.RenameColumn(
                name: "Created_By",
                table: "Message",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Message",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Message",
                newName: "Created_By");

            migrationBuilder.RenameColumn(
                name: "Created_At",
                table: "Message",
                newName: "Created_On");
        }
    }
}
