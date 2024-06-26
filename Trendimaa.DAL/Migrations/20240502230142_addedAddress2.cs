using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trendimaa.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedAddress2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_AppUsers_AppUserId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Address_AddressId",
                table: "Sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Address_AppUserId",
                table: "Addresses",
                newName: "IX_Addresses_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AppUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Addresses_AddressId",
                table: "Sellers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AppUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellers_Addresses_AddressId",
                table: "Sellers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_AppUserId",
                table: "Address",
                newName: "IX_Address_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AppUsers_AppUserId",
                table: "Address",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellers_Address_AddressId",
                table: "Sellers",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
