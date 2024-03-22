using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedSearchRelated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainHomeCarousel",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainHomeSpace",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AddressTopic",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SearchRelateds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRelateds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchRelateds_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SearchRelateds_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SearchRelateds_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_CampaignId",
                table: "Images",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchRelateds_AppUserId",
                table: "SearchRelateds",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchRelateds_CategoryId",
                table: "SearchRelateds",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchRelateds_ProductId",
                table: "SearchRelateds",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Campaigns_CampaignId",
                table: "Images",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Campaigns_CampaignId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "SearchRelateds");

            migrationBuilder.DropIndex(
                name: "IX_Images_CampaignId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsMainHomeCarousel",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "IsMainHomeSpace",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "AddressTopic",
                table: "Address");
        }
    }
}
