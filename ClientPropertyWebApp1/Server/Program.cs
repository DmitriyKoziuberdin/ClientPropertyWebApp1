global using ClientPropertyWebApp1.Shared;
global using Microsoft.EntityFrameworkCore;
global using ClientPropertyWebApp1.Server.Data;
using Microsoft.AspNetCore.ResponseCompression;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        //builder.Services.AddDbContext<AppDbContext>(options =>
        //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))) - by using MS SQL;

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("AppDbContext"));
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
        app.UseRouting();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}