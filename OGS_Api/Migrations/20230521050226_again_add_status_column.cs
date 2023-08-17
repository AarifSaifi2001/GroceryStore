using Microsoft.EntityFrameworkCore.Migrations;

namespace OGS_Api.Migrations
{
    public partial class again_add_status_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "orderstatus",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderstatus",
                table: "Orders");
        }
    }
}
