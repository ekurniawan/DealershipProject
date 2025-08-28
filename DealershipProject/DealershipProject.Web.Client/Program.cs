using DealershipProject.Shared.Services;
using DealershipProject.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the DealershipProject.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<ICarService, CarService>();

await builder.Build().RunAsync();
