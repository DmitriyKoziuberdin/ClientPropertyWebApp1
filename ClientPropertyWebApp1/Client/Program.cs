global using ClientPropertyWebApp1.Client.Services.PropertyService;
global using ClientPropertyWebApp1.Client.Services.UserService;
global using ClientPropertyWebApp1.Shared;
using ClientPropertyWebApp1.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddScoped<IPropertyService, PropertyService>();
        builder.Services.AddScoped<IUserService, UserService>();

        await builder.Build().RunAsync();
    }
}