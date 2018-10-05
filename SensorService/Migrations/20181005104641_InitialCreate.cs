using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SensorService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:timescaledb", "'timescaledb', '', ''");

            migrationBuilder.CreateTable(
                name: "Sensors",
                schema: "public",
                columns: table => new
                {
                    SensorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureSensorReadings",
                schema: "public",
                columns: table => new
                {
                    SensorReadingId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Time = table.Column<DateTime>(nullable: false),
                    Reading = table.Column<double>(nullable: false),
                    SensorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureSensorReadings", x => x.SensorReadingId);
                    table.ForeignKey(
                        name: "FK_TemperatureSensorReadings_Sensors_SensorId",
                        column: x => x.SensorId,
                        principalSchema: "public",
                        principalTable: "Sensors",
                        principalColumn: "SensorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureSensorReadings_SensorId",
                schema: "public",
                table: "TemperatureSensorReadings",
                column: "SensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemperatureSensorReadings",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Sensors",
                schema: "public");
        }
    }
}
