using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainDude.Queries.Data.Migrations
{
    /// <inheritdoc />
    public partial class AFewSmallFixesForTrivialIssues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SegmentStation_NameGerman",
                table: "SegmentStation");

            migrationBuilder.AlterColumn<double>(
                name: "NominalLength",
                table: "Segments",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "NominalLength",
                table: "Segments",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SegmentStation_NameGerman",
                table: "SegmentStation",
                column: "NameGerman",
                unique: true);
        }
    }
}
