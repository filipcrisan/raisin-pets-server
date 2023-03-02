using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raisinpets.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAvatarUrlToBase64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "Pets",
                newName: "AvatarInBase64");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarInBase64",
                table: "Pets",
                newName: "AvatarUrl");
        }
    }
}
