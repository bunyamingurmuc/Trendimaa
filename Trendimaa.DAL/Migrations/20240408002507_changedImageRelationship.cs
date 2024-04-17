using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedImageRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Images_ImageId",
                table: "Sellers");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_ImageId",
                table: "Sellers");

            migrationBuilder.CreateIndex(
                name: "IX_Images_SellerId",
                table: "Images",
                column: "SellerId",
                unique: true,
                filter: "[SellerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Sellers_SellerId",
                table: "Images",
                column: "SellerId",
                principalTable: "Sellers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Sellers_SellerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_SellerId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Sellers_ImageId",
                table: "Sellers",
                column: "ImageId",
                unique: true,
                filter: "[ImageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Images_ImageId",
                table: "Sellers",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
