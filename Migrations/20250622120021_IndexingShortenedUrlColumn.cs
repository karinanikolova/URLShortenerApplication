using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortenerApp.Migrations
{
    /// <inheritdoc />
    public partial class IndexingShortenedUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_URLs_ShortenedUrl",
                table: "URLs",
                column: "ShortenedUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_URLs_ShortenedUrl",
                table: "URLs");
        }
    }
}
