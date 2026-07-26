using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDude.Data.Migrations
{
    /// <inheritdoc />
    public partial class SmallRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteExtremes_Routes_SegmentId",
                table: "RouteExtremes");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteExtremes_Stations_StationId",
                table: "RouteExtremes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteExtremes",
                table: "RouteExtremes");

            migrationBuilder.RenameTable(
                name: "RouteExtremes",
                newName: "RouteEnds");

            migrationBuilder.RenameIndex(
                name: "IX_RouteExtremes_StationId",
                table: "RouteEnds",
                newName: "IX_RouteEnds_StationId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RouteEnds_Routes_SegmentId",
                table: "RouteEnds");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteEnds_Stations_StationId",
                table: "RouteEnds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RouteEnds",
                table: "RouteEnds");

            migrationBuilder.RenameTable(
                name: "RouteEnds",
                newName: "RouteExtremes");

            migrationBuilder.RenameIndex(
                name: "IX_RouteEnds_StationId",
                table: "RouteExtremes",
                newName: "IX_RouteExtremes_StationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RouteExtremes",
                table: "RouteExtremes",
                columns: new[] { "SegmentId", "IsEnd" });

            migrationBuilder.AddForeignKey(
                name: "FK_RouteExtremes_Routes_SegmentId",
                table: "RouteExtremes",
                column: "SegmentId",
                principalTable: "Routes",
                principalColumn: "SegmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteExtremes_Stations_StationId",
                table: "RouteExtremes",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
