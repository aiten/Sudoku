using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sudoku.Repository.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Application = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Level = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    MachineName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Port = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServerAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RemoteAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Logger = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sudoku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    LastStored = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sudoku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sudoku_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sudoku_CategoryId",
                table: "Sudoku",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Sudoku");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
