using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.MSSqlData
{
    /// <inheritdoc />
    public partial class CreateMSSqlDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passwords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    InitVect = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: true),
                    Url = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Name = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserName = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Password = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Note = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordReprompt = table.Column<bool>(type: "bit", nullable: false),
                    AutoLogin = table.Column<bool>(type: "bit", nullable: false),
                    DisableAutofill = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passwords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmtpSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Host = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    User = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    From = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    UseSSL = table.Column<bool>(type: "bit", nullable: false),
                    UseHTML = table.Column<bool>(type: "bit", nullable: false),
                    DefaultTitle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Signature = table.Column<string>(type: "nvarchar(max)", maxLength: 4096, nullable: true),
                    UseDefaultCredentials = table.Column<bool>(type: "bit", nullable: false),
                    TimeOut = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmtpSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreationTime", "Description", "DisplayOrder", "MainCategoryId", "Name", "Published" },
                values: new object[] { 1, new DateTime(2023, 5, 1, 4, 9, 7, 862, DateTimeKind.Local).AddTicks(2814), "Varsayılan Kategori", 0, 0, "Genel", true });

            migrationBuilder.InsertData(
                table: "SmtpSettings",
                columns: new[] { "Id", "CreationTime", "DefaultTitle", "From", "Host", "Password", "Port", "SenderName", "Signature", "TimeOut", "UseDefaultCredentials", "UseHTML", "UseSSL", "User" },
                values: new object[] { 1, new DateTime(2023, 5, 1, 4, 9, 7, 861, DateTimeKind.Local).AddTicks(9201), "Password Manager", "your@email.net", "smtp.yandex.com.tr", "your password", 587, "Password Manager", "", 15000, false, true, true, "your@email.net" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Passwords");

            migrationBuilder.DropTable(
                name: "PaymentCards");

            migrationBuilder.DropTable(
                name: "SmtpSettings");
        }
    }
}
