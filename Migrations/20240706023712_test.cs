using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbBlogger1.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b18be9c0-aa65-4af8-bd17-00bd9344e576", "a18be9c0-aa65-4af8-bd17-00bd9344e575" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "01930cfe-ad27-492f-b7be-a36183e5519b", "AQAAAAIAAYagAAAAECPRyYXmOmXQ0vMPTLmMYRckW1iwRozGVJPNWtTFNa35JY4zoIOPjZTltP9jeaKx7w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b18be9c0-aa65-4af8-bd17-00bd9344e576", "a18be9c0-aa65-4af8-bd17-00bd9344e575" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "726fdb56-c580-41e1-a23f-814acc0114e5", "AQAAAAIAAYagAAAAEGKjqZti3KHFlNnDH+MbmuHM3SWOXZtAqkXEvwhrydqPEsOD2LWr4nPCJrACCRcYKA==" });
        }
    }
}
