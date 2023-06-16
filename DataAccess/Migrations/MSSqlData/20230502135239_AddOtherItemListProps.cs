using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.MSSqlData
{
    /// <inheritdoc />
    public partial class AddOtherItemListProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CardNumber",
                table: "PaymentCards",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CardOwner",
                table: "PaymentCards",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CardType",
                table: "PaymentCards",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PaymentCards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "ExprationDate",
                table: "PaymentCards",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "InitVect",
                table: "PaymentCards",
                type: "varbinary(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PaymentCards",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Note",
                table: "PaymentCards",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PasswordReprompt",
                table: "PaymentCards",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "SecurityCode",
                table: "PaymentCards",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "InitVect",
                table: "Passwords",
                type: "varbinary(55)",
                maxLength: 55,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "BankName",
                table: "BankAccounts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "Iban",
                table: "BankAccounts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "InitVect",
                table: "BankAccounts",
                type: "varbinary(55)",
                maxLength: 55,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BankAccounts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Note",
                table: "BankAccounts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PasswordReprompt",
                table: "BankAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "SwiftCode",
                table: "BankAccounts",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Addresses",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Addresses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "Addresses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Addresses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Addresses",
                type: "nvarchar(max)",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PasswordReprompt",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SmtpSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2023, 5, 2, 16, 52, 39, 491, DateTimeKind.Local).AddTicks(8858));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "CardOwner",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "CardType",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "ExprationDate",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "InitVect",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "PasswordReprompt",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "PaymentCards");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Iban",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "InitVect",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "PasswordReprompt",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "SwiftCode",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PasswordReprompt",
                table: "Addresses");

            migrationBuilder.AlterColumn<byte[]>(
                name: "InitVect",
                table: "Passwords",
                type: "varbinary(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(55)",
                oldMaxLength: 55,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "SmtpSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationTime",
                value: new DateTime(2023, 5, 2, 4, 21, 36, 356, DateTimeKind.Local).AddTicks(6752));
        }
    }
}
