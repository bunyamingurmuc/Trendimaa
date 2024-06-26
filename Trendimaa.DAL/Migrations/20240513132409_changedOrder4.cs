using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedOrder4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderImage",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "AppUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "AppUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "OrderImage",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
