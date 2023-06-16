using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.MSSqlData
{
    /// <inheritdoc />
    public partial class ENotesAddProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Notes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Notes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PasswordReprompt",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2023, 5, 1, 11, 24, 44, 826, DateTimeKind.Local).AddTicks(8315));

            migrationBuilder.UpdateData(
                table: "SmtpSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2023, 5, 1, 11, 24, 44, 826, DateTimeKind.Local).AddTicks(5740));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "PasswordReprompt",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationTime", "MainCategoryId" },
                values: new object[] { new DateTime(2023, 5, 1, 4, 9, 7, 862, DateTimeKind.Local).AddTicks(2814), 0 });

            migrationBuilder.UpdateData(
                table: "SmtpSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2023, 5, 1, 4, 9, 7, 861, DateTimeKind.Local).AddTicks(9201));
        }
    }
}
