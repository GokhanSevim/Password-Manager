using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySql;
using MySql.Data;
using MySql.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.MySQL.Contexts
{
    public class MySqlIdentityDataContext : IdentityDbContext<ApplicationUser>
    {
        public MySqlIdentityDataContext(DbContextOptions<MySqlIdentityDataContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
