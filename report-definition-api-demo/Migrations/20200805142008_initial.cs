using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace report_definition_api_demo.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReportDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportIdentitty = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Engine = table.Column<string>(nullable: true),
                    Definition = table.Column<string>(nullable: true),
                    Permission = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDefinitions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportDefinitions");
        }
    }
}
