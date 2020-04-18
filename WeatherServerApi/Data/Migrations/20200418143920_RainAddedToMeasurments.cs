using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherServerApi.Data.Migrations
{
    public partial class RainAddedToMeasurments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RainSpeed",
                table: "Measurements",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RainSpeed",
                table: "Measurements");
        }
    }
}
