using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevLounge.Data.Migrations
{
    public partial class Added_Thread_Views : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Threads");
        }
    }
}
