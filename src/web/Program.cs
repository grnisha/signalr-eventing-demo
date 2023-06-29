using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using BlazorApp.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var baseAPIUri = builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAPIUri ?? builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new HubConnectionBuilder()
    .WithUrl(baseAPIUri+ "/api/demoHub")
    .WithAutomaticReconnect()
    .Build());

await builder.Build().RunAsync();
