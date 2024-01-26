using ShopTARge22.Data;
using Microsoft.EntityFrameworkCore;
using ShopTARge22.Core.ServiceInterface;
using ShopTARge22.ApplicationServices.Services;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using ShopTARge22.Core.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISpaceshipsServices, SpaceshipsServices>();
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IKindergartensServices, KindergartensServices>();
builder.Services.AddScoped<IRealEstatesServices, RealEstatesServices>();
builder.Services.AddScoped<IAccuWeatherForecastsServices, AccuWeatherForecastsServices>();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ShopTARge22Context>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
//{
//	options.SignIn.RequireConfirmedAccount = true;
//	options.Password.RequiredLength = 3;

//	options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
//	options.Lockout.MaxFailedAccessAttempts = 3;
//	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
//})
//	.AddEntityFrameworkStores<ShopTARge22Context>()
//	.AddDefaultTokenProviders()
//	.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
//	.AddDefaultUI();

////all tokens
//builder.Services.Configure<DataProtectionTokenProviderOptions>
//	(o => o.TokenLifespan = TimeSpan.FromHours(5));
//builder.Services.AddAuthentication()
//	.AddFacebook(options =>
//	{
//		options.AppId = "1726613754502075";
//		options.AppSecret = "6d810b6de16a3cc65ac3ed31af259bf3";
//	})
//	.AddGoogle(options =>
//	{
//		options.ClientId = "653663266336-kv6l3lfqg666ufian0ccrh55anf6a10q.apps.googleusercontent.com";
//		options.ClientSecret = "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";
//	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider
	(Path.Combine(builder.Environment.ContentRootPath, "multipleFileUpload")),
	RequestPath = "/multipleFileUpload"
});
;

app.UseRouting();

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapRazorPages();

//landing page
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
