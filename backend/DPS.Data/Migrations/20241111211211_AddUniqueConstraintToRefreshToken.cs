using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DPS.Data
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousRefreshToken",
                table: "UserRefreshTokens",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_RefreshToken",
                table: "UserRefreshTokens",
                column: "RefreshToken",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRefreshTokens_RefreshToken",
                table: "UserRefreshTokens");

            migrationBuilder.DropColumn(
                name: "PreviousRefreshToken",
                table: "UserRefreshTokens");
        }
    }
}
