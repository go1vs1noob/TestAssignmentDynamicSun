using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentDynamicSun.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAddWeatherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<double>(type: "float", nullable: false),
                    Humidity = table.Column<double>(type: "float", nullable: true),
                    DewPoint = table.Column<double>(type: "float", nullable: true),
                    AirPressure = table.Column<int>(type: "int", nullable: true),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WindSpeed = table.Column<int>(type: "int", nullable: true),
                    Cloudiness = table.Column<int>(type: "int", nullable: true),
                    CloudBase = table.Column<int>(type: "int", nullable: true),
                    HorizontalVisibility = table.Column<int>(type: "int", nullable: true),
                    NaturalPhenomena = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
