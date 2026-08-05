using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDude.Queries.Data.Migrations
{
    /// <inheritdoc />
    public partial class WriteToReadProjection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Radii");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "SegmentStation");

            migrationBuilder.CreateTable(
                name: "RadiusAggregates",
                columns: table => new
                {
                    RadiusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false),
                    Minimum = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadiusAggregates", x => x.RadiusId);
                });

            migrationBuilder.CreateTable(
                name: "StationAggregates",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "INTEGER", nullable: false),
                    NameGerman = table.Column<string>(type: "TEXT", nullable: false),
                    NameGermanNew = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationAggregates", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "StationEntities",
                columns: table => new
                {
                    SegmentStationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StationId = table.Column<int>(type: "INTEGER", nullable: false),
                    NameGerman = table.Column<string>(type: "TEXT", nullable: false),
                    NameGermanNew = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationEntities", x => x.SegmentStationId);
                });

            migrationBuilder.CreateTable(
                name: "SegmentAggregates",
                columns: table => new
                {
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    NominalLength = table.Column<double>(type: "REAL", nullable: true),
                    ASegmentStationId = table.Column<int>(type: "INTEGER", nullable: false),
                    BSegmentStationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentAggregates", x => x.SegmentId);
                    table.ForeignKey(
                        name: "FK_SegmentAggregates_StationEntities_ASegmentStationId",
                        column: x => x.ASegmentStationId,
                        principalTable: "StationEntities",
                        principalColumn: "SegmentStationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SegmentAggregates_StationEntities_BSegmentStationId",
                        column: x => x.BSegmentStationId,
                        principalTable: "StationEntities",
                        principalColumn: "SegmentStationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SegmentAggregates_ASegmentStationId",
                table: "SegmentAggregates",
                column: "ASegmentStationId");

            migrationBuilder.CreateIndex(
                name: "IX_SegmentAggregates_BSegmentStationId",
                table: "SegmentAggregates",
                column: "BSegmentStationId");

            migrationBuilder.CreateIndex(
                name: "IX_StationAggregates_NameGerman",
                table: "StationAggregates",
                column: "NameGerman",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RadiusAggregates");

            migrationBuilder.DropTable(
                name: "SegmentAggregates");

            migrationBuilder.DropTable(
                name: "StationAggregates");

            migrationBuilder.DropTable(
                name: "StationEntities");

            migrationBuilder.CreateTable(
                name: "Radii",
                columns: table => new
                {
                    RadiusId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Minimum = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radii", x => x.RadiusId);
                });

            migrationBuilder.CreateTable(
                name: "SegmentStation",
                columns: table => new
                {
                    SegmentStationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameGerman = table.Column<string>(type: "TEXT", nullable: false),
                    NameGermanNew = table.Column<string>(type: "TEXT", nullable: true),
                    StationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentStation", x => x.SegmentStationId);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameGerman = table.Column<string>(type: "TEXT", nullable: false),
                    NameGermanNew = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ASegmentStationId = table.Column<int>(type: "INTEGER", nullable: false),
                    BSegmentStationId = table.Column<int>(type: "INTEGER", nullable: false),
                    NominalLength = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.SegmentId);
                    table.ForeignKey(
                        name: "FK_Segments_SegmentStation_ASegmentStationId",
                        column: x => x.ASegmentStationId,
                        principalTable: "SegmentStation",
                        principalColumn: "SegmentStationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Segments_SegmentStation_BSegmentStationId",
                        column: x => x.BSegmentStationId,
                        principalTable: "SegmentStation",
                        principalColumn: "SegmentStationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Segments_ASegmentStationId",
                table: "Segments",
                column: "ASegmentStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_BSegmentStationId",
                table: "Segments",
                column: "BSegmentStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_NameGerman",
                table: "Stations",
                column: "NameGerman",
                unique: true);
        }
    }
}
