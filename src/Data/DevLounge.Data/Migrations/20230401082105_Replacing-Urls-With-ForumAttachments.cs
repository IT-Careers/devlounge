using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLounge.Data.Migrations
{
    public partial class ReplacingUrlsWithForumAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ThumbnailImageUrl",
                table: "Categories");

            migrationBuilder.AddColumn<long>(
                name: "CoverImageId",
                table: "Categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ThumbnailImageId",
                table: "Categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CoverImageId",
                table: "Categories",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ThumbnailImageId",
                table: "Categories",
                column: "ThumbnailImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Attachments_CoverImageId",
                table: "Categories",
                column: "CoverImageId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Attachments_ThumbnailImageId",
                table: "Categories",
                column: "ThumbnailImageId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Attachments_CoverImageId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Attachments_ThumbnailImageId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CoverImageId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ThumbnailImageId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CoverImageId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ThumbnailImageId",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailImageUrl",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
