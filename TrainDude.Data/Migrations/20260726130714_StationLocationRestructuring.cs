using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDude.Data.Migrations
{
    /// <inheritdoc />
    public partial class StationLocationRestructuring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteExtremes_Routes_RouteId",
                table: "RouteExtremes");

            migrationBuilder.DropColumn(
                name: "Location_Latitude",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "Location_Longitude",
                table: "Stations");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Stations",
                newName: "StationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Routes",
                newName: "SegmentId");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "RouteExtremes",
                newName: "SegmentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Radii",
                newName: "RadiusId");

            migrationBuilder.CreateTable(
                name: "StationLocation",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationLocation", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_StationLocation_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RouteExtremes_Routes_SegmentId",
                table: "RouteExtremes",
                column: "SegmentId",
                principalTable: "Routes",
                principalColumn: "SegmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteExtremes_Routes_SegmentId",
                table: "RouteExtremes");

            migrationBuilder.DropTable(
                name: "StationLocation");

            migrationBuilder.RenameColumn(
                name: "StationId",
                table: "Stations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SegmentId",
                table: "Routes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SegmentId",
                table: "RouteExtremes",
                newName: "RouteId");

            migrationBuilder.RenameColumn(
                name: "RadiusId",
                table: "Radii",
                newName: "Id");

            migrationBuilder.AddColumn<double>(
                name: "Location_Latitude",
                table: "Stations",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Location_Longitude",
                table: "Stations",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteExtremes_Routes_RouteId",
                table: "RouteExtremes",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
