using Microsoft.EntityFrameworkCore.Migrations;

namespace ElPlatform.DAL.Migrations
{
    public partial class elplatform2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParantId",
                table: "MediaItemCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParantMediaItemCategoryId",
                table: "MediaItemCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaItemCategories_ParantMediaItemCategoryId",
                table: "MediaItemCategories",
                column: "ParantMediaItemCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaItemCategories_MediaItemCategories_ParantMediaItemCategoryId",
                table: "MediaItemCategories",
                column: "ParantMediaItemCategoryId",
                principalTable: "MediaItemCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaItemCategories_MediaItemCategories_ParantMediaItemCategoryId",
                table: "MediaItemCategories");

            migrationBuilder.DropIndex(
                name: "IX_MediaItemCategories_ParantMediaItemCategoryId",
                table: "MediaItemCategories");

            migrationBuilder.DropColumn(
                name: "ParantMediaItemCategoryId",
                table: "MediaItemCategories");

            migrationBuilder.AlterColumn<int>(
                name: "ParantId",
                table: "MediaItemCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
