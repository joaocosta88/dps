using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DPS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateListingEntityAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_OwnerId",
                table: "Listings");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Listings",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Listings",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_Listings_OwnerId",
                table: "Listings",
                newName: "IX_Listings_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_AuthorId",
                table: "Listings",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Listings_AspNetUsers_AuthorId",
                table: "Listings");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Listings",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Listings",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Listings_AuthorId",
                table: "Listings",
                newName: "IX_Listings_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_AspNetUsers_OwnerId",
                table: "Listings",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
