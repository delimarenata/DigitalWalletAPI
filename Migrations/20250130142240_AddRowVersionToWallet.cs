using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalWalletAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionToWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Wallets",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Wallets");
        }
    }
}
