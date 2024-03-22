using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedSubSubCategoryandSubCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "SearchRelateds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubSubCategoryId",
                table: "SearchRelateds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfClicks",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubSubCategoryId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Campaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SubCategoies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategoies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategoies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubSubCategories_SubCategoies_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategoies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchRelateds_SubCategoryId",
                table: "SearchRelateds",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchRelateds_SubSubCategoryId",
                table: "SearchRelateds",
                column: "SubSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubCategoryId",
                table: "Product",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubSubCategoryId",
                table: "Product",
                column: "SubSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategoies_CategoryId",
                table: "SubCategoies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubSubCategories_SubCategoryId",
                table: "SubSubCategories",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SubCategoies_SubCategoryId",
                table: "Product",
                column: "SubCategoryId",
                principalTable: "SubCategoies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_SubSubCategories_SubSubCategoryId",
                table: "Product",
                column: "SubSubCategoryId",
                principalTable: "SubSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchRelateds_SubCategoies_SubCategoryId",
                table: "SearchRelateds",
                column: "SubCategoryId",
                principalTable: "SubCategoies",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchRelateds_SubSubCategories_SubSubCategoryId",
                table: "SearchRelateds",
                column: "SubSubCategoryId",
                principalTable: "SubSubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_SubCategoies_SubCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_SubSubCategories_SubSubCategoryId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchRelateds_SubCategoies_SubCategoryId",
                table: "SearchRelateds");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchRelateds_SubSubCategories_SubSubCategoryId",
                table: "SearchRelateds");

            migrationBuilder.DropTable(
                name: "SubSubCategories");

            migrationBuilder.DropTable(
                name: "SubCategoies");

            migrationBuilder.DropIndex(
                name: "IX_SearchRelateds_SubCategoryId",
                table: "SearchRelateds");

            migrationBuilder.DropIndex(
                name: "IX_SearchRelateds_SubSubCategoryId",
                table: "SearchRelateds");

            migrationBuilder.DropIndex(
                name: "IX_Product_SubCategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_SubSubCategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "SearchRelateds");

            migrationBuilder.DropColumn(
                name: "SubSubCategoryId",
                table: "SearchRelateds");

            migrationBuilder.DropColumn(
                name: "NumberOfClicks",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SubSubCategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Campaigns");
        }
    }
}
