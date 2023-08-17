using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OGS_Api.Migrations
{
    public partial class User_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    password = table.Column<byte[]>(nullable: true),
                    passwordKey = table.Column<byte[]>(nullable: true),
                    mobileno = table.Column<int>(nullable: false),
                    address = table.Column<string>(nullable: true),
                    imageUrl = table.Column<string>(nullable: true),
                    publicId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
