using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DPS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToListing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Listings",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Listings",
                newName: "IsDeleted");
        }
    }
}
