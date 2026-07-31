using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDude.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChartSegment");

            migrationBuilder.DropTable(
                name: "Charts");

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    LineId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.LineId);
                });

            migrationBuilder.CreateTable(
                name: "LineSegment",
                columns: table => new
                {
                    LineId = table.Column<string>(type: "TEXT", nullable: false),
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineSegment", x => new { x.SegmentId, x.LineId });
                    table.ForeignKey(
                        name: "FK_LineSegment_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "LineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineSegment_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "SegmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineSegment_LineId",
                table: "LineSegment",
                column: "LineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineSegment");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.CreateTable(
                name: "Charts",
                columns: table => new
                {
                    ChartId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charts", x => x.ChartId);
                });

            migrationBuilder.CreateTable(
                name: "ChartSegment",
                columns: table => new
                {
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChartId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartSegment", x => new { x.SegmentId, x.ChartId });
                    table.ForeignKey(
                        name: "FK_ChartSegment_Charts_ChartId",
                        column: x => x.ChartId,
                        principalTable: "Charts",
                        principalColumn: "ChartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartSegment_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "SegmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChartSegment_ChartId",
                table: "ChartSegment",
                column: "ChartId");
        }
    }
}
