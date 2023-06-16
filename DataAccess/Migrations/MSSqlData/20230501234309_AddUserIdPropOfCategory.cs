using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.MSSqlData
{
    /// <inheritdoc />
    public partial class AddUserIdPropOfCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Categories",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SmtpSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "DefaultTitle", "SenderName" },
                values: new object[] { new DateTime(2023, 5, 2, 2, 43, 8, 955, DateTimeKind.Local).AddTicks(147), "Şifre Yönetimi", "Şifre Yönetimi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreationTime", "Description", "DisplayOrder", "Name", "Published" },
                values: new object[] { 1, new DateTime(2023, 5, 2, 2, 2, 40, 519, DateTimeKind.Local).AddTicks(7456), "Varsayılan Kategori", 0, "Genel", true });

            migrationBuilder.UpdateData(
                table: "SmtpSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "DefaultTitle", "SenderName" },
                values: new object[] { new DateTime(2023, 5, 2, 2, 2, 40, 519, DateTimeKind.Local).AddTicks(4793), "Password Manager", "Password Manager" });
        }
    }
}
