using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeightTracker;
using WeightTracker.Services;
using WeightTracker.Services.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<IUserService, UserServiceServer>();
builder.Services.AddSingleton<IWeightService, WeightServiceServer>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();