using CelilCavus.Data.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<CokkieContext>(x =>
        {
            x.UseSqlServer("Server=.;Database=AspEFCore_CustomCokkieIdentity;Trusted_Connection=True;TrustServerCertificate=True;");
        });
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(x =>
        {
            x.Cookie.Name = "CustomCokkie";
            x.Cookie.HttpOnly = true;
            x.Cookie.SameSite = SameSiteMode.Strict;
            x.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            // x.LoginPath = new PathString("");
            // x.LogoutPath = new PathString("Home/LogOut");

        });
        builder.Services.AddControllersWithViews();


        var app = builder.Build();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapDefaultControllerRoute();

        app.Run();
    }
}