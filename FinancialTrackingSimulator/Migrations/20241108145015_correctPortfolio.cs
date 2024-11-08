using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialTrackingSimulator.Migrations
{
    /// <inheritdoc />
    public partial class correctPortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyChange",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Portfolios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DailyChange",
                table: "Portfolios",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Portfolios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Portfolios",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
