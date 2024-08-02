using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbBlogger1.Migrations
{
    /// <inheritdoc />
    public partial class kot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "237edf3f-3741-4bc3-881e-0ef40d549040", "AQAAAAIAAYagAAAAEMVNU9YfQybILnPwejR5EoHaV9jJm6AA9EydDDYPq8yBaKoAnvtJjmZFAzxsVySbPg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aec04e94-5aa9-4528-a576-3ada6b922c0a", "AQAAAAIAAYagAAAAED/dSOGUGAi8BhmqZrJrvLuD/Gfz9z2L5b3rp8qi1V79jVv6/nVa3xEpG/AdyJ8UNQ==" });
        }
    }
}
