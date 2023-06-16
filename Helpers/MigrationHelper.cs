using DataAccess.Concrete.EntityFramework.MSSQL.Contexts;
using DataAccess.Concrete.EntityFramework.MySQL.Contexts;
using DataAccess.Concrete.EntityFramework.PostgreSQL.Contexts;
using Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Helpers
{
    /// <summary>
    /// Entity Framework Core kullanarak ASP.NET Core'da veritabanı geçişlerini uygulamak için yardımcı bir yöntem.
    /// </summary>
    [TryException]
	public static class MigrationHelper
	{
		public static void ApplyMigrations(WebApplication app, string preferredDatabase)
		{
			switch (preferredDatabase)
			{
				case "mysql":
					ApplyMySqlMigrations(app);
					break;
				case "postgresql":
					ApplyPostgreSqlMigrations(app);
					break;
				default:
					ApplyMsSqlMigrations(app);
					break;
			}
		}

		private static void ApplyMsSqlMigrations(WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetService<IdentityDataContext>();
				dbContext.Database.Migrate();
			}

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetService<MSSqlDataContext>();
				dbContext.Database.Migrate();
			}
		}

		private static void ApplyMySqlMigrations(WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetService<MySqlIdentityDataContext>();
				dbContext.Database.EnsureCreated();
				dbContext.Database.Migrate();
			}

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetService<MySqlDataContext>();
				dbContext.Database.EnsureCreated();
				dbContext.Database.Migrate();
			}
		}

		private static void ApplyPostgreSqlMigrations(WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetService<PostgreSqlIdentityDataContext>();
				dbContext.Database.EnsureCreated();
				dbContext.Database.Migrate();
			}

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetService<PostgreSqlDataContext>();
				dbContext.Database.EnsureCreated();
				dbContext.Database.Migrate();
			}
		}
	}
}
