using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SensorService.Migrations
{
    public partial class WeatherSensorCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherSensorReading",
                schema: "public",
                columns: table => new
                {
                    SensorReadingId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Temperature = table.Column<double>(nullable: false),
                    Pressure = table.Column<double>(nullable: false),
                    Humidity = table.Column<double>(nullable: false),
                    MinimumTemperature = table.Column<double>(nullable: false),
                    MaximumTemperature = table.Column<double>(nullable: false),
                    ConditionCode = table.Column<int>(nullable: false),
                    Condition = table.Column<string>(nullable: true),
                    IconURL = table.Column<string>(nullable: true),
                    SensorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherSensorReading", x => x.SensorReadingId);
                    table.ForeignKey(
                        name: "FK_WeatherSensorReading_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalSchema: "public",
                        principalTable: "Sensors",
                        principalColumn: "SensorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherSensorReading_SensorId",
                schema: "public",
                table: "WeatherSensorReading",
                column: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherSensorReading",
                schema: "public");
        }
    }
}
