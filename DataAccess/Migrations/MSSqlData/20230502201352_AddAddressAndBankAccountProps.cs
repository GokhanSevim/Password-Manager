using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.MSSqlData
{
    /// <inheritdoc />
    public partial class AddAddressAndBankAccountProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "AccountNumber",
                table: "BankAccounts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Addresses",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SmtpSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2023, 5, 2, 23, 13, 52, 258, DateTimeKind.Local).AddTicks(5737));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Addresses");

            migrationBuilder.UpdateData(
                table: "SmtpSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2023, 5, 2, 16, 52, 39, 491, DateTimeKind.Local).AddTicks(8858));
        }
    }
}
