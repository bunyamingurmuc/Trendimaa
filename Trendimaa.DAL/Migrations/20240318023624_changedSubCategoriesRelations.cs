using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changedSubCategoriesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoies_Categories_CategoryId",
                table: "SubCategoies");

            migrationBuilder.DropForeignKey(
                name: "FK_SubSubCategories_SubCategoies_SubCategoryId",
                table: "SubSubCategories");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Specifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Sellers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Images",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoies_Categories_CategoryId",
                table: "SubCategoies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SubSubCategories_SubCategoies_SubCategoryId",
                table: "SubSubCategories",
                column: "SubCategoryId",
                principalTable: "SubCategoies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Images_ImageId",
                table: "Sellers");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategoies_Categories_CategoryId",
                table: "SubCategoies");

            migrationBuilder.DropForeignKey(
                name: "FK_SubSubCategories_SubCategoies_SubCategoryId",
                table: "SubSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Sellers_ImageId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Images");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategoies_Categories_CategoryId",
                table: "SubCategoies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubSubCategories_SubCategoies_SubCategoryId",
                table: "SubSubCategories",
                column: "SubCategoryId",
                principalTable: "SubCategoies",
                principalColumn: "Id");
        }
    }
}
