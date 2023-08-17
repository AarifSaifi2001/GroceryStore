using Microsoft.EntityFrameworkCore.Migrations;

namespace OGS_Api.Migrations
{
    public partial class add_publicId_to_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "publicId",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "publicId",
                table: "Categories");
        }
    }
}
