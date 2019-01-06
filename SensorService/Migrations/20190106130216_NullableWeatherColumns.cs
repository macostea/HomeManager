using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SensorService.Migrations
{
    public partial class NullableWeatherColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Pressure",
                schema: "public",
                table: "WeatherSensorReading",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Humidity",
                schema: "public",
                table: "WeatherSensorReading",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "ConditionCode",
                schema: "public",
                table: "WeatherSensorReading",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                schema: "public",
                table: "WeatherSensorReading",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                schema: "public",
                table: "WeatherSensorReading");

            migrationBuilder.AlterColumn<double>(
                name: "Pressure",
                schema: "public",
                table: "WeatherSensorReading",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Humidity",
                schema: "public",
                table: "WeatherSensorReading",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConditionCode",
                schema: "public",
                table: "WeatherSensorReading",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
