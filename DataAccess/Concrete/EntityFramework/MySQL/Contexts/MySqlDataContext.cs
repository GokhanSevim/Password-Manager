using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.MySQL.Contexts
{
    public class MySqlDataContext : DbContext
    {
        private readonly string connStr = "";

        public MySqlDataContext()
        {
            if (string.IsNullOrWhiteSpace(connStr))
            {
                var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                var Configuration = builder.Build();
                connStr = Configuration["ConnectionStrings:MySqlDefaultConnection"];
            }
        }

        public MySqlDataContext(DbContextOptions<MySqlDataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrWhiteSpace(connStr))
            {
                optionsBuilder.UseMySQL(connStr);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var smtp = new ESmtpSettings()
            {
                Id = 1,
                Host = "smtp.yandex.com.tr",
                UseDefaultCredentials = false,
                UseSSL = true,
                SenderName = "Password Manager",
                From = "your@email.net",
                User = "your@email.net",
                Password = "your password",
                DefaultTitle = "Password Manager",
                Port = 587,
                TimeOut = 15000,
                UseHTML = true,
                Signature = "",
                CreationTime = DateTime.Now
            };

            modelBuilder.Entity<ESmtpSettings>().HasData(smtp);
            modelBuilder.Entity<EPasswords>();
            modelBuilder.Entity<ENotes>();
            modelBuilder.Entity<EAddresses>();
            modelBuilder.Entity<EBankAccounts>();
            modelBuilder.Entity<EPaymentCards>();
            modelBuilder.Entity<ECategories>();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ESmtpSettings> SmtpSettings { get; set; }
        public DbSet<EPasswords> Passwords { get; set; }
        public DbSet<ENotes> Notes { get; set; }
        public DbSet<EAddresses> Addresses { get; set; }
        public DbSet<EBankAccounts> BankAccounts { get; set; }
        public DbSet<EPaymentCards> PaymentCards { get; set; }
        public DbSet<ECategories> Categories { get; set; }
    }
}
