using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobRecommendationWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkCV",
                table: "CV");

            migrationBuilder.AlterColumn<int>(
                name: "MaBaiDang",
                table: "PHIEUTOCAO",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte[]>(
                name: "AnhCongTy",
                table: "HOSOCONGTY",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "AnhCV",
                table: "CV",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnhCongTy",
                table: "HOSOCONGTY");

            migrationBuilder.DropColumn(
                name: "AnhCV",
                table: "CV");

            migrationBuilder.AlterColumn<int>(
                name: "MaBaiDang",
                table: "PHIEUTOCAO",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkCV",
                table: "CV",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
