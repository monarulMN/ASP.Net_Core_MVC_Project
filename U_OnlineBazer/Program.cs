using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using U_OnlineBazar.Models;
using U_OnlineBazer.Data;

internal class Program
{
    private static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorPages();

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));


        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        builder.Services.AddDistributedMemoryCache();
        //builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            //options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseSession();
        app.UseAuthorization();
        app.UseCookiePolicy();
        app.UseAuthentication();


        app.MapControllerRoute(
              name: "areas",
              pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
            );
        app.MapControllerRoute(
              name: "areas",
              pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}"
            );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}




