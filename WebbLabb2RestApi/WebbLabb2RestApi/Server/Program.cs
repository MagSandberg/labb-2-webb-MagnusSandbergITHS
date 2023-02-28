using Microsoft.EntityFrameworkCore;
using WebbLabb2RestApi.DataAccess.Repositories;
using WebbLabb2RestApi.DataAccess.Sql.Contexts;
using WebbLabb2RestApi.DataAccess.Sql.Repositories;
using WebbLabb2RestApi.Server.Extensions;
using WebbLabb2RestApi.Server.Services;
using WebbLabb2RestApi.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderRepository>();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CustomerRepository>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("CustomerDb")
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapMongoDbProductEndpoints();
app.MapMongoDbOrderEndpoints();
app.MapSqlDbEndpoints();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
