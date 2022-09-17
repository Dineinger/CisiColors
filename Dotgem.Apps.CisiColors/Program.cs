using Dotgem.Apps.CisiColors;
using Dotgem.Apps.CisiColors.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped(sp => new ColorReaderHttp(sp.GetRequiredService<HttpClient>(), basePath: "colors"));

await builder.Build().RunAsync();
