using DataAccess.Concrete.EntityFramework.MSSQL.Contexts;
using DataAccess.Concrete.EntityFramework.MySQL.Contexts;
using DataAccess.Concrete.EntityFramework.PostgreSQL.Contexts;
using Entities;
using Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Helpers
{
	[TryException]
	public static class DatabaseContextHelper
	{
		public static void ApplyContextConfigurations(WebApplicationBuilder builder, string preferredDatabase)
		{
			switch (preferredDatabase)
			{
				case "mysql":

					//kullanıcı hesabı oluşturma gereksinimlerini belirliyoruz
					builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
					{
						options.SignIn.RequireConfirmedAccount = false;
						options.SignIn.RequireConfirmedEmail = false;
						options.Password.RequireDigit = true;
						options.Password.RequiredLength = 12;
						options.Password.RequireLowercase = true;
						options.Password.RequireNonAlphanumeric = false;
						options.Password.RequireUppercase = true;
					})
					.AddEntityFrameworkStores<MySqlIdentityDataContext>();

					builder.Services.AddDbContext<MySqlIdentityDataContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlIdentityConnection")));

					builder.Services.AddDbContext<MySqlDataContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySqlDefaultConnection")));

					break;
				case "postgresql":

					//kullanıcı hesabı oluşturma gereksinimlerini belirliyoruz
					builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
					{
						options.SignIn.RequireConfirmedAccount = false;
						options.SignIn.RequireConfirmedEmail = false;
						options.Password.RequireDigit = true;
						options.Password.RequiredLength = 12;
						options.Password.RequireLowercase = true;
						options.Password.RequireNonAlphanumeric = false;
						options.Password.RequireUppercase = true;
					})
					.AddEntityFrameworkStores<PostgreSqlIdentityDataContext>();

					builder.Services.AddDbContext<PostgreSqlIdentityDataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlIdentityConnection")));

					builder.Services.AddDbContext<PostgreSqlDataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlDefaultConnection")));

					break;
				default:

					//kullanıcı hesabı oluşturma gereksinimlerini belirliyoruz
					builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
					{
						options.SignIn.RequireConfirmedAccount = false;
						options.SignIn.RequireConfirmedEmail = false;
						options.Password.RequireDigit = true;
						options.Password.RequiredLength = 12;
						options.Password.RequireLowercase = true;
						options.Password.RequireNonAlphanumeric = false;
						options.Password.RequireUppercase = true;
					})
					.AddEntityFrameworkStores<IdentityDataContext>();

					//Kullanıcı hesabı ve oturum açma bilgilerinin tutulduğu veri tabanı. 
					builder.Services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

					//Kullanıcının arşivlediği özel bilgilerin tutulduğu veri tabanı
					builder.Services.AddDbContext<MSSqlDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
					break;
			}

		}
	}
}
