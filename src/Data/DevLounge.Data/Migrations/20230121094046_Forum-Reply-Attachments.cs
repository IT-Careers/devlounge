using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLounge.Data.Migrations
{
    public partial class ForumReplyAttachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Replies_ForumReplyId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_ForumReplyId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "ForumReplyId",
                table: "Attachments");

            migrationBuilder.CreateTable(
                name: "ForumReplyAttachment",
                columns: table => new
                {
                    ReplyId = table.Column<long>(type: "bigint", nullable: false),
                    AttachmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumReplyAttachment", x => new { x.ReplyId, x.AttachmentId });
                    table.ForeignKey(
                        name: "FK_ForumReplyAttachment_Attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForumReplyAttachment_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumReplyAttachment_AttachmentId",
                table: "ForumReplyAttachment",
                column: "AttachmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumReplyAttachment");

            migrationBuilder.AddColumn<long>(
                name: "ForumReplyId",
                table: "Attachments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_ForumReplyId",
                table: "Attachments",
                column: "ForumReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Replies_ForumReplyId",
                table: "Attachments",
                column: "ForumReplyId",
                principalTable: "Replies",
                principalColumn: "Id");
        }
    }
}
