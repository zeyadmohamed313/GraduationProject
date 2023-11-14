using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ToReads_UserId",
                table: "ToReads");

            migrationBuilder.DropIndex(
                name: "IX_Reads_UserId",
                table: "Reads");

            migrationBuilder.CreateIndex(
                name: "IX_ToReads_UserId",
                table: "ToReads",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reads_UserId",
                table: "Reads",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ToReads_UserId",
                table: "ToReads");

            migrationBuilder.DropIndex(
                name: "IX_Reads_UserId",
                table: "Reads");

            migrationBuilder.CreateIndex(
                name: "IX_ToReads_UserId",
                table: "ToReads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reads_UserId",
                table: "Reads",
                column: "UserId");
        }
    }
}
