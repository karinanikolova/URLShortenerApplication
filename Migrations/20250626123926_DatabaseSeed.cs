using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace URLShortenerApp.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "URLs",
                columns: new[] { "Id", "CreationDate", "OriginalUrl", "ShortenedUrl" },
                values: new object[,]
                {
                    { new Guid("19cbaea2-e65e-4931-8c25-685708d7f4c8"), new DateTime(2025, 5, 22, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(1363), "https://www.telerik.com/", "X9k2v3Q8yF" },
                    { new Guid("e244bc69-5e82-4cdc-aabf-69a7c620d282"), new DateTime(2025, 6, 1, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(1025), "https://www.progress.com/", "NlptN4Bw1P" },
                    { new Guid("f2fd801f-6493-4a93-a9fa-1829c8ac21fd"), new DateTime(2025, 5, 27, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(1358), "https://www.dev.bg/company/progress/", "BtGWzHZ96t" }
                });

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "AccessDate", "URLId", "UserIPAddress" },
                values: new object[,]
                {
                    { new Guid("149c2f2c-14ec-4169-bb66-7e8c014c78ac"), new DateTime(2025, 6, 25, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7309), new Guid("e244bc69-5e82-4cdc-aabf-69a7c620d282"), "165.122.32.209" },
                    { new Guid("1eaa56a1-99c6-4c5c-8351-58f03a381c47"), new DateTime(2025, 6, 21, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7322), new Guid("19cbaea2-e65e-4931-8c25-685708d7f4c8"), "128.181.26.173" },
                    { new Guid("1f034183-ec79-4ea2-a0c1-8bd47967efff"), new DateTime(2025, 6, 19, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7316), new Guid("f2fd801f-6493-4a93-a9fa-1829c8ac21fd"), "165.122.32.209" },
                    { new Guid("37898beb-b7b7-4398-ab01-96596bedee10"), new DateTime(2025, 6, 17, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7310), new Guid("e244bc69-5e82-4cdc-aabf-69a7c620d282"), "1.225.63.20" },
                    { new Guid("6051d816-2a0c-439f-90ac-cba5b7d913df"), new DateTime(2025, 6, 25, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7323), new Guid("19cbaea2-e65e-4931-8c25-685708d7f4c8"), "165.122.32.209" },
                    { new Guid("86739286-18d5-4f31-85b6-f6d824108650"), new DateTime(2025, 6, 26, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(6583), new Guid("e244bc69-5e82-4cdc-aabf-69a7c620d282"), "165.122.32.209" },
                    { new Guid("8b91667d-031a-4c26-8817-a4c05fbca351"), new DateTime(2025, 6, 23, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7308), new Guid("e244bc69-5e82-4cdc-aabf-69a7c620d282"), "119.240.93.167" },
                    { new Guid("8f55796e-b0dd-451e-a22e-8335af736b6d"), new DateTime(2025, 6, 11, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7314), new Guid("f2fd801f-6493-4a93-a9fa-1829c8ac21fd"), "215.148.253.91" },
                    { new Guid("8f602fd7-869a-484a-9cf2-6f41d4dacbad"), new DateTime(2025, 6, 25, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7318), new Guid("f2fd801f-6493-4a93-a9fa-1829c8ac21fd"), "215.148.253.91" },
                    { new Guid("d1c80305-ec15-4a89-b96a-b8a4dd259959"), new DateTime(2025, 6, 24, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7317), new Guid("f2fd801f-6493-4a93-a9fa-1829c8ac21fd"), "165.218.50.241" },
                    { new Guid("e13642b1-c894-41d3-acc9-8211e8a2ba8b"), new DateTime(2025, 6, 6, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7320), new Guid("19cbaea2-e65e-4931-8c25-685708d7f4c8"), "119.240.93.167" },
                    { new Guid("e2723ab2-6c56-43e4-b594-a59f9a3b9006"), new DateTime(2025, 6, 16, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7305), new Guid("e244bc69-5e82-4cdc-aabf-69a7c620d282"), "165.122.32.209" },
                    { new Guid("fad2151d-5e15-4b6c-9db3-fd9149217e7e"), new DateTime(2025, 6, 1, 12, 39, 25, 984, DateTimeKind.Utc).AddTicks(7321), new Guid("19cbaea2-e65e-4931-8c25-685708d7f4c8"), "165.218.50.241" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("149c2f2c-14ec-4169-bb66-7e8c014c78ac"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("1eaa56a1-99c6-4c5c-8351-58f03a381c47"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("1f034183-ec79-4ea2-a0c1-8bd47967efff"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("37898beb-b7b7-4398-ab01-96596bedee10"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("6051d816-2a0c-439f-90ac-cba5b7d913df"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("86739286-18d5-4f31-85b6-f6d824108650"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("8b91667d-031a-4c26-8817-a4c05fbca351"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("8f55796e-b0dd-451e-a22e-8335af736b6d"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("8f602fd7-869a-484a-9cf2-6f41d4dacbad"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("d1c80305-ec15-4a89-b96a-b8a4dd259959"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("e13642b1-c894-41d3-acc9-8211e8a2ba8b"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("e2723ab2-6c56-43e4-b594-a59f9a3b9006"));

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: new Guid("fad2151d-5e15-4b6c-9db3-fd9149217e7e"));

            migrationBuilder.DeleteData(
                table: "URLs",
                keyColumn: "Id",
                keyValue: new Guid("19cbaea2-e65e-4931-8c25-685708d7f4c8"));

            migrationBuilder.DeleteData(
                table: "URLs",
                keyColumn: "Id",
                keyValue: new Guid("e244bc69-5e82-4cdc-aabf-69a7c620d282"));

            migrationBuilder.DeleteData(
                table: "URLs",
                keyColumn: "Id",
                keyValue: new Guid("f2fd801f-6493-4a93-a9fa-1829c8ac21fd"));
        }
    }
}
