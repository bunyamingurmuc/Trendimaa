using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedSubSubCategorySpecificationRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Varieties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Specifications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubSubCategoryId",
                table: "Specifications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_SubSubCategoryId",
                table: "Specifications",
                column: "SubSubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_SubSubCategories_SubSubCategoryId",
                table: "Specifications",
                column: "SubSubCategoryId",
                principalTable: "SubSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_SubSubCategories_SubSubCategoryId",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_SubSubCategoryId",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Varieties");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "SubSubCategoryId",
                table: "Specifications");
        }
    }
}
