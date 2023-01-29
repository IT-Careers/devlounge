using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLounge.Data.Migrations
{
    public partial class ThreadCategoryRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Threads",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CategoryId",
                table: "Threads",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Categories_CategoryId",
                table: "Threads",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Categories_CategoryId",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Threads_CategoryId",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Threads");
        }
    }
}
