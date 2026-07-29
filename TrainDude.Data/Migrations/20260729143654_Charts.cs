using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDude.Data.Migrations
{
    /// <inheritdoc />
    public partial class Charts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    ChartId = table.Column<string>(type: "TEXT", nullable: false),
                    SegmentId = table.Column<int>(type: "INTEGER", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChartSegment");

            migrationBuilder.DropTable(
                name: "Charts");
        }
    }
}
