using Microsoft.EntityFrameworkCore;
using WebbLabb2RestApi.DataAccess.Repositories;
using WebbLabb2RestApi.DataAccess.Sql.Contexts;
using WebbLabb2RestApi.Server.Extensions;
using WebbLabb2RestApi.Server.Services;
using WebbLabb2RestApi.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

app.MapMongoDbEndpoints();

//app.MapPost("/createProduct", async (ProductService productService, ProductDto productDto) =>
//    await productService.AddProduct(productDto));

////Ändra till Put och ta in en Dto
//app.MapPatch("/updateProduct", async (ProductService productService, string name, string value) =>
//    await productService.UpdateProduct(name, value));

//app.MapPatch("/updateAvailability", async (ProductService productService, string name, bool value) =>
//    await productService.UpdateAvailability(name, value));

//app.MapGet("/getAllProducts", async (ProductService productService) =>
//    await productService.GetProducts());

//app.MapGet("/getProductByName", async (ProductService productService, string name) =>
//    await productService.GetProductByName(name));

//app.MapDelete("/removeProduct", async (ProductService productService, string name) =>
//    await productService.RemoveProduct(name));

//app.MapGet("/getAllCustomers", async (CustomerRepository customerRepository) => 
//    await customerRepository.GetAllCustomers());

//app.MapGet("/getAllOrders", async (OrderRepository orderRepository) => 
//    await orderRepository.GetAllOrders());

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
