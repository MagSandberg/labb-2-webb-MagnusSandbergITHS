using MongoDB.Bson;
using WebbLabb2RestApi.DataAccess.Repositories;
using WebbLabb2RestApi.Server.Services;
using WebbLabb2RestApi.Shared.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

app.MapPost("/createProduct", async (ProductService productService, ProductDto productDto) =>
    await productService.AddProduct(productDto));

app.MapGet("/getAllProducts", async (ProductService productService) =>
    await productService.GetProducts());

app.MapGet("/getProductByName", async (ProductService productService, string name) =>
    await productService.GetProductByName(name));

//app.MapGet("/getAllCustomers", async (CustomerRepository customerRepository) => 
//    await customerRepository.GetAllCustomers());

//app.MapGet("/getAllOrders", async (OrderRepository orderRepository) => 
//    await orderRepository.GetAllOrders());

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
