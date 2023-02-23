using RESTApi.Shared.DTOs;
using RESTApi.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<ProductDto>();
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

app.MapGet("/createProduct", async (ProductRepository productRepository, ProductDto dto) =>
    await productRepository.CreateProduct(dto));

//app.MapGet("/getAllCustomers", async (CustomerRepository customerRepository) => 
//    await customerRepository.GetAllCustomers());

//app.MapGet("/getAllOrders", async (OrderRepository orderRepository) => 
//    await orderRepository.GetAllOrders());

//app.MapGet("/getAllProducts", async (ProductRepository productRepository) => 
//    await productRepository.GetAllProducts());

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
