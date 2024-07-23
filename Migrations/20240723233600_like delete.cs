using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbBlogger1.Migrations
{
    /// <inheritdoc />
    public partial class likedelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6e35def2-6884-429b-b190-b2912541517c", "AQAAAAIAAYagAAAAEKUj+UzWNs1kQHwHnma0cRiDhn0tiCgsOaJK55xvgdTx9PLzY/6IeEhPAtYukGLwgw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2128c602-3cfa-44a1-81bf-4dd7a679b67c", "AQAAAAIAAYagAAAAECPr279vdRWg4BnUl7BijGz1BfJj33qEIWebpqZB87mCfb29gDm74NI3yiMuM0ui5Q==" });
        }
    }
}
