using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.PostgreSQL.Contexts
{
    public class PostgreSqlIdentityDataContext : IdentityDbContext<ApplicationUser>
    {
        public PostgreSqlIdentityDataContext(DbContextOptions<PostgreSqlIdentityDataContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
