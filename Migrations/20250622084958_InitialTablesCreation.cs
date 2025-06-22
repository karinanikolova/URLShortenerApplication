using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortenerApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialTablesCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "URLs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "URL identifier"),
                    OriginalUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false, comment: "Original URL"),
                    ShortenedUrl = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "Shortened URL"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "URL creation date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URLs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Record identifier"),
                    AccessDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "URL access date"),
                    UserIPAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false, comment: "User IP adress"),
                    URLId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "URL record identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Records_URLs_URLId",
                        column: x => x.URLId,
                        principalTable: "URLs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Records_URLId",
                table: "Records",
                column: "URLId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "URLs");
        }
    }
}
