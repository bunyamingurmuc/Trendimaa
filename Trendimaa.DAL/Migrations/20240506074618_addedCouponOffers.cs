using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedCouponOffers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponOfferId",
                table: "Coupons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CouponOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    RemainingSeconds = table.Column<long>(type: "bigint", nullable: true),
                    Piece = table.Column<int>(type: "int", nullable: true),
                    CouponAmount = table.Column<double>(type: "float", nullable: false),
                    MinOrderAmount = table.Column<double>(type: "float", nullable: true),
                    IsOneTime = table.Column<bool>(type: "bit", nullable: false),
                    IsJustForSeller = table.Column<bool>(type: "bit", nullable: false),
                    IsForFirstTimeUser = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellerCoupon",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    CouponId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerCoupon", x => new { x.SellerId, x.CouponId });
                    table.ForeignKey(
                        name: "FK_SellerCoupon_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellerCoupon_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerCouponOffer",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    CouponOfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerCouponOffer", x => new { x.CouponOfferId, x.SellerId });
                    table.ForeignKey(
                        name: "FK_SellerCouponOffer_CouponOffers_CouponOfferId",
                        column: x => x.CouponOfferId,
                        principalTable: "CouponOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellerCouponOffer_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_CouponOfferId",
                table: "Coupons",
                column: "CouponOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerCoupon_CouponId",
                table: "SellerCoupon",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerCouponOffer_SellerId",
                table: "SellerCouponOffer",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_CouponOffers_CouponOfferId",
                table: "Coupons",
                column: "CouponOfferId",
                principalTable: "CouponOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_CouponOffers_CouponOfferId",
                table: "Coupons");

            migrationBuilder.DropTable(
                name: "SellerCoupon");

            migrationBuilder.DropTable(
                name: "SellerCouponOffer");

            migrationBuilder.DropTable(
                name: "CouponOffers");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_CouponOfferId",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "CouponOfferId",
                table: "Coupons");
        }
    }
}
