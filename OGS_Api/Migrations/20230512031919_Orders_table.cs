using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OGS_Api.Migrations
{
    public partial class Orders_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderNumber = table.Column<string>(nullable: true),
                    productName = table.Column<string>(nullable: true),
                    productPrice = table.Column<double>(nullable: false),
                    productQuantity = table.Column<string>(nullable: true),
                    invoiceId = table.Column<string>(nullable: true),
                    userId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
