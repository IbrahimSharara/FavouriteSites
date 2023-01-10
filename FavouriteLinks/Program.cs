using FavouriteLinks.Models;
using FavouriteLinks.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FavouriteSitesContext>(options =>
{
    options.UseSqlServer("Server=.;Database=FavouriteSites;Trusted_Connection=True;");
    //option.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));
});
builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
 .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
 {
     options.LoginPath = new PathString("/Account/login");
     options.AccessDeniedPath = new PathString("/Home/Error");
 });
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 5;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
})
            .AddEntityFrameworkStores<FavouriteSitesContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
