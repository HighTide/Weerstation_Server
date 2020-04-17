using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherServerApi.Data.Migrations
{
    public partial class PressureToMeasurments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Pressure",
                table: "Measurements",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pressure",
                table: "Measurements");
        }
    }
}
