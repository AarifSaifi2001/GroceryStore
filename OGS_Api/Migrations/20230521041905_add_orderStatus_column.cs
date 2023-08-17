using Microsoft.EntityFrameworkCore.Migrations;

namespace OGS_Api.Migrations
{
    public partial class add_orderStatus_column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "orderStatus",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "orderStatus",
                table: "Orders");
        }
    }
}
