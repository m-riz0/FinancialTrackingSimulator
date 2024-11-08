using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialTrackingSimulator.Migrations
{
    /// <inheritdoc />
    public partial class correctedwatchlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyChange",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Watchlists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DailyChange",
                table: "Watchlists",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Watchlists",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Watchlists",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
