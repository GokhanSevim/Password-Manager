using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using PM.UI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
	//geliþtirme ortamýnda sql hatalarýnýn ayrýntýlý görünmesini saðlar
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

var userPreferences = builder.Configuration.GetSection("UserPreferences");
var preferredDatabase = userPreferences["PreferredDatabase"];

Helpers.DatabaseContextHelper.ApplyContextConfigurations(builder, preferredDatabase);

builder.Services.Configure<RouteOptions>(options =>
{
	options.AppendTrailingSlash = false;
	options.SuppressCheckForUnhandledSecurityMetadata = true;
});

builder.Services.AddResponseCompression(options =>
{
	options.EnableForHttps = true;
	options.MimeTypes = new[]
	 {
		 "text/plain",
		 "text/html",
		 "text/css",
		 "font/woff2",
		 "application/javascript",
		 "image/x-icon",
		 "image/png"
	 };
	options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
	options.Level = CompressionLevel.Optimal;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
	options.CheckConsentNeeded = context => false;
	options.MinimumSameSitePolicy = SameSiteMode.Strict;
	options.Secure = CookieSecurePolicy.Always;
});

//ApplicationUser oturum bilgisini tutmak için kullanýlýyor.
builder.Services.AddSession(options =>
{
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
	options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	options.Cookie.SameSite = SameSiteMode.Strict;
	options.Cookie.MaxAge = TimeSpan.FromDays(360);
	options.IdleTimeout = TimeSpan.FromDays(360);
});

//kullanýcý oturum açma ve kapatma sayfalarýný belirliyoruz
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/account/login/";
	options.LogoutPath = "/account/logout/";
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.AddHttpContextAccessor();

#region DI Configuration

switch (preferredDatabase)
{
	case "mysql":
		DependencyResolvers.Microsoft.MySQL.DependencyInjectionConfig.Configure(builder.Services);
		break;
	case "postgresql":
		DependencyResolvers.Microsoft.PostgreSQL.DependencyInjectionConfig.Configure(builder.Services);
		break;
	default:
		DependencyResolvers.Microsoft.MSSQL.DependencyInjectionConfig.Configure(builder.Services);
		break;
}

#endregion

var app = builder.Build();

#region Apply Migrations

Helpers.MigrationHelper.ApplyMigrations(app, preferredDatabase);

#endregion

//hata sayfalarýný özelleþtirilmesini saðlýyoruz.
app.UseStatusCodePagesWithReExecute("/{0}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
	OnPrepareResponse = ctx =>
	{
		ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={60 * 60 * 24 * 180}");
	}
});

app.UseSession();
app.UseCookiePolicy();
app.UseRouting();
app.UseResponseCaching();
app.UseResponseCompression();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.UseMiddleware<SecurityHeadersMiddleware>();
app.UseMiddleware<LanguageMiddleware>();

app.Run();
