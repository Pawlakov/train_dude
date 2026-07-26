using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDude.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoutesRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteEnds_Routes_SegmentId",
                table: "RouteEnds");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteEnds_Stations_StationId",
                table: "RouteEnds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Routes",
                table: "Routes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteEnds",
                table: "RouteEnds");

            migrationBuilder.RenameTable(
                name: "Routes",
                newName: "Segments");

            migrationBuilder.RenameTable(
                name: "RouteEnds",
                newName: "SegmentExtremes");

            migrationBuilder.RenameIndex(
                name: "IX_RouteEnds_StationId",
                table: "SegmentExtremes",
                newName: "IX_SegmentExtremes_StationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Segments",
                table: "Segments",
                column: "SegmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SegmentExtremes",
                table: "SegmentExtremes",
                columns: new[] { "SegmentId", "IsEnd" });

            migrationBuilder.AddForeignKey(
                name: "FK_SegmentExtremes_Segments_SegmentId",
                table: "SegmentExtremes",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "SegmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SegmentExtremes_Stations_StationId",
                table: "SegmentExtremes",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SegmentExtremes_Segments_SegmentId",
                table: "SegmentExtremes");

            migrationBuilder.DropForeignKey(
                name: "FK_SegmentExtremes_Stations_StationId",
                table: "SegmentExtremes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Segments",
                table: "Segments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SegmentExtremes",
                table: "SegmentExtremes");

            migrationBuilder.RenameTable(
                name: "Segments",
                newName: "Routes");

            migrationBuilder.RenameTable(
                name: "SegmentExtremes",
                newName: "RouteEnds");

            migrationBuilder.RenameIndex(
                name: "IX_SegmentExtremes_StationId",
                table: "RouteEnds",
                newName: "IX_RouteEnds_StationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Routes",
                table: "Routes",
                column: "SegmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteEnds",
                table: "RouteEnds",
                columns: new[] { "SegmentId", "IsEnd" });

            migrationBuilder.AddForeignKey(
                name: "FK_RouteEnds_Routes_SegmentId",
                table: "RouteEnds",
                column: "SegmentId",
                principalTable: "Routes",
                principalColumn: "SegmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteEnds_Stations_StationId",
                table: "RouteEnds",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
