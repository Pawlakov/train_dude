using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDude.Commands.Data.Migrations
{
    /// <inheritdoc />
    public partial class CleanSlate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    LineNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    LineLetter = table.Column<char>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => new { x.LineNumber, x.LineLetter });
                });

            migrationBuilder.CreateTable(
                name: "Radii",
                columns: table => new
                {
                    RadiusId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false),
                    Minimum = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radii", x => x.RadiusId);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NominalLength = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.SegmentId);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameGerman = table.Column<string>(type: "TEXT", nullable: false),
                    NameGermanNew = table.Column<string>(type: "TEXT", nullable: true),
                    NamePolish = table.Column<string>(type: "TEXT", nullable: true),
                    NamePolishOld = table.Column<string>(type: "TEXT", nullable: true),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripNumber = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripNumber);
                });

            migrationBuilder.CreateTable(
                name: "LineSegments",
                columns: table => new
                {
                    LineNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    LineLetter = table.Column<char>(type: "TEXT", nullable: false),
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineSegments", x => new { x.SegmentId, x.LineNumber, x.LineLetter });
                    table.ForeignKey(
                        name: "FK_LineSegments_Lines_LineNumber_LineLetter",
                        columns: x => new { x.LineNumber, x.LineLetter },
                        principalTable: "Lines",
                        principalColumns: new[] { "LineNumber", "LineLetter" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineSegments_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "SegmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SegmentVertices",
                columns: table => new
                {
                    OrdinalId = table.Column<int>(type: "INTEGER", nullable: false),
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentVertices", x => new { x.SegmentId, x.OrdinalId });
                    table.ForeignKey(
                        name: "FK_SegmentVertices_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "SegmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SegmentExtremes",
                columns: table => new
                {
                    IsEnd = table.Column<bool>(type: "INTEGER", nullable: false),
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    StationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SegmentExtremes", x => new { x.SegmentId, x.IsEnd });
                    table.ForeignKey(
                        name: "FK_SegmentExtremes_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "SegmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SegmentExtremes_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineSegments_LineNumber_LineLetter",
                table: "LineSegments",
                columns: new[] { "LineNumber", "LineLetter" });

            migrationBuilder.CreateIndex(
                name: "IX_SegmentExtremes_StationId",
                table: "SegmentExtremes",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_NameGerman",
                table: "Stations",
                column: "NameGerman",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineSegments");

            migrationBuilder.DropTable(
                name: "Radii");

            migrationBuilder.DropTable(
                name: "SegmentExtremes");

            migrationBuilder.DropTable(
                name: "SegmentVertices");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Segments");
        }
    }
}
