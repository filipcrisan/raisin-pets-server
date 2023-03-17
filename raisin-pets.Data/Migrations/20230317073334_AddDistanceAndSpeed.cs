using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raisinpets.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDistanceAndSpeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AverageSpeed",
                table: "Exercises",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalDistance",
                table: "Exercises",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageSpeed",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "TotalDistance",
                table: "Exercises");
        }
    }
}
