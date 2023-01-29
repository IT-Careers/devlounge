using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLounge.Data.Migrations
{
    public partial class FixedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AspNetUsers_CreatedById1",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById1",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_CreatedById1",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_AspNetUsers_CreatedById1",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AspNetUsers_CreatedById1",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Threads_CreatedById1",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Sections_CreatedById1",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Replies_CreatedById1",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedById1",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_CreatedById1",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Attachments");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Threads",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Sections",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Replies",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Attachments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CreatedById",
                table: "Threads",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CreatedById",
                table: "Sections",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_CreatedById",
                table: "Replies",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CreatedById",
                table: "Attachments",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_AspNetUsers_CreatedById",
                table: "Attachments",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_CreatedById",
                table: "Replies",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_AspNetUsers_CreatedById",
                table: "Sections",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AspNetUsers_CreatedById",
                table: "Threads",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AspNetUsers_CreatedById",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_CreatedById",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_AspNetUsers_CreatedById",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_AspNetUsers_CreatedById",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Threads_CreatedById",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Sections_CreatedById",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Replies_CreatedById",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_CreatedById",
                table: "Attachments");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedById",
                table: "Threads",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById1",
                table: "Threads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedById",
                table: "Sections",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById1",
                table: "Sections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedById",
                table: "Replies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById1",
                table: "Replies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedById",
                table: "Categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById1",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatedById",
                table: "Attachments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById1",
                table: "Attachments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CreatedById1",
                table: "Threads",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CreatedById1",
                table: "Sections",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_CreatedById1",
                table: "Replies",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById1",
                table: "Categories",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_CreatedById1",
                table: "Attachments",
                column: "CreatedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_AspNetUsers_CreatedById1",
                table: "Attachments",
                column: "CreatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById1",
                table: "Categories",
                column: "CreatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_CreatedById1",
                table: "Replies",
                column: "CreatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_AspNetUsers_CreatedById1",
                table: "Sections",
                column: "CreatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_AspNetUsers_CreatedById1",
                table: "Threads",
                column: "CreatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
