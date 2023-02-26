using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using WebbLabb2RestApi.Client;
using WebbLabb2RestApi.DataAccess.Sql.Contexts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddDbContext<CustomerContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("CustomerDb")
));

await builder.Build().RunAsync();
