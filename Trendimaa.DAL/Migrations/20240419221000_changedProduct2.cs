using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfirmStatus",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmStatus",
                table: "Products");
        }
    }
}
