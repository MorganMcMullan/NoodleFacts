using Microsoft.EntityFrameworkCore.Migrations;

namespace RileyBot.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facts",
                columns: table => new
                {
                    FactID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FactText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facts", x => x.FactID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facts");
        }
    }
}
