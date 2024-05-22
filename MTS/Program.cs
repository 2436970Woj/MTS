global using Application.Interfaces.EmployeeServiceInterfaces;
global using Application.Interfaces.RoleServiceInterfaces;
global using Application.Services.EmployeeServices;
global using Application.Services.RoleServices;
global using Infrastructure.Repositories;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MTS;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

await builder.Build().RunAsync();
