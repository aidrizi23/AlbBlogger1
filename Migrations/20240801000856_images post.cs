using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbBlogger1.Migrations
{
    /// <inheritdoc />
    public partial class imagespost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Posts",
                newName: "Images");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e3ed7c61-347f-4f87-a7b8-479cfaff10ca", "AQAAAAIAAYagAAAAEJ83Q8iZYo6h3u29Ojja8LW4PUjEEGB2vu5/WAdcdBFkWNWTv2yX5BnkLMlObT4QKA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Posts",
                newName: "Image");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e575",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "237edf3f-3741-4bc3-881e-0ef40d549040", "AQAAAAIAAYagAAAAEMVNU9YfQybILnPwejR5EoHaV9jJm6AA9EydDDYPq8yBaKoAnvtJjmZFAzxsVySbPg==" });
        }
    }
}
