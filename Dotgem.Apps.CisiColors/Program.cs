using Dotgem.Apps.CisiColors;
using Dotgem.Apps.CisiColors.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton(_ => new ColorPaths("colors"));
builder.Services.AddScoped<IColorReader, ColorReaderHttp>();

await builder.Build().RunAsync();
