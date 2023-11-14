using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CurrentlyReadings");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ReadId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToReadId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reads_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToReads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToReads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToReads_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ReadId",
                table: "Books",
                column: "ReadId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ToReadId",
                table: "Books",
                column: "ToReadId");

            migrationBuilder.CreateIndex(
                name: "IX_Reads_UserId",
                table: "Reads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ToReads_UserId",
                table: "ToReads",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Reads_ReadId",
                table: "Books",
                column: "ReadId",
                principalTable: "Reads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ToReads_ToReadId",
                table: "Books",
                column: "ToReadId",
                principalTable: "ToReads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Reads_ReadId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_ToReads_ToReadId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Reads");

            migrationBuilder.DropTable(
                name: "ToReads");

            migrationBuilder.DropIndex(
                name: "IX_Books_ReadId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ToReadId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReadId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ToReadId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CurrentlyReadings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
