using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernSchool.Worker.Migrations
{
    public partial class FixNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Teachers_TeacherId",
                table: "Class");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Class",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Teachers_TeacherId",
                table: "Class",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Class_Teachers_TeacherId",
                table: "Class");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Class",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Class_Teachers_TeacherId",
                table: "Class",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
