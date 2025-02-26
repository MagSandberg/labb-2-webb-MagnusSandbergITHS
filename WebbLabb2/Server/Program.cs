using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebbLabb2.Client.Shared;
using WebbLabb2.DataAccess.Repositories;
using WebbLabb2.DataAccess.Sql.Contexts;
using WebbLabb2.DataAccess.Sql.Repositories;
using WebbLabb2.Server.Data;
using WebbLabb2.Server.Extensions;
using WebbLabb2.Server.Models;
using WebbLabb2.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderRepository>();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CustomerRepository>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();

//builder.Services.AddScoped<UserManager<IdentityUser>>();
//builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<UserRepository>();

builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("CustomerDb")
));

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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

app.UseIdentityServer();
app.UseAuthorization();

app.MapMongoDbProductEndpoints();
app.MapMongoDbOrderEndpoints();
app.MapSqlDbCustomerEndpoints();
//app.MapSqlDbUserEndpoints();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();