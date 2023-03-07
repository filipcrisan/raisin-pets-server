using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raisinpets.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddContentToTutorials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Tutorials",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Tutorials");
        }
    }
}
