using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedMuchChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Varieties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "Varieties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Varieties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubSubCategoryId",
                table: "Varieties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "Specifications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Specifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Specifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyFullName",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Varieties_SubCategoryId",
                table: "Varieties",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Varieties_SubSubCategoryId",
                table: "Varieties",
                column: "SubSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_SubCategoryId",
                table: "Specifications",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_SubCategoies_SubCategoryId",
                table: "Specifications",
                column: "SubCategoryId",
                principalTable: "SubCategoies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Varieties_SubCategoies_SubCategoryId",
                table: "Varieties",
                column: "SubCategoryId",
                principalTable: "SubCategoies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Varieties_SubSubCategories_SubSubCategoryId",
                table: "Varieties",
                column: "SubSubCategoryId",
                principalTable: "SubSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_SubCategoies_SubCategoryId",
                table: "Specifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Varieties_SubCategoies_SubCategoryId",
                table: "Varieties");

            migrationBuilder.DropForeignKey(
                name: "FK_Varieties_SubSubCategories_SubSubCategoryId",
                table: "Varieties");

            migrationBuilder.DropIndex(
                name: "IX_Varieties_SubCategoryId",
                table: "Varieties");

            migrationBuilder.DropIndex(
                name: "IX_Varieties_SubSubCategoryId",
                table: "Varieties");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_SubCategoryId",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Varieties");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "Varieties");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Varieties");

            migrationBuilder.DropColumn(
                name: "SubSubCategoryId",
                table: "Varieties");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "CompanyFullName",
                table: "Sellers");
        }
    }
}
