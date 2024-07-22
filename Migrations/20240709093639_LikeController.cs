using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbBlogger1.Migrations
{
    /// <inheritdoc />
    public partial class LikeController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4694597d-3e5f-41f6-a479-af1bd8b1c306", "AQAAAAIAAYagAAAAEJKZwA52MuPnNVg4BaW5So84hmiGIxuDgnR70c3wTUWvKa3a/986dEKS8aYC6X3Cpw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "43601def-50a6-4ef1-8761-76ae7777e09e", "AQAAAAIAAYagAAAAEDreqmXpa5blxEcYW6FgTAyjYh8BOQPK1dyvG05kDCY9x2nHOjCnZKlQ3NkouDV48g==" });
        }
    }
}
