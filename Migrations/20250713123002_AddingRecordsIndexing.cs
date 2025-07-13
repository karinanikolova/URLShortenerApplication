using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortenerApp.Migrations
{
    /// <inheritdoc />
    public partial class AddingRecordsIndexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_URLId",
                table: "Records");

            migrationBuilder.CreateIndex(
                name: "IX_Records_URLId_AccessDate_UserIPAddress",
                table: "Records",
                columns: new[] { "URLId", "AccessDate", "UserIPAddress" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Records_URLId_AccessDate_UserIPAddress",
                table: "Records");

            migrationBuilder.CreateIndex(
                name: "IX_Records_URLId",
                table: "Records",
                column: "URLId");
        }
    }
}
