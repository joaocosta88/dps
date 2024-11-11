using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DPS.Data
{
    /// <inheritdoc />
    public partial class AddIsSessionTokenToUserRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSessionToken",
                table: "UserRefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSessionToken",
                table: "UserRefreshTokens");
        }
    }
}
