using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationProductEndpointExtensions
{
    public static WebApplication MapMongoDbProductEndpoints(this WebApplication app)
    {
        app.MapPost("/createProduct", async (ProductService productService, ProductDto dto) =>
        {
            await productService.AddProduct(dto);
            return Results.Text("Product successfully added");
        });

        app.MapPatch("/updateProduct", async (ProductService productService, string id, ProductDto dto) =>
        {
            await productService.UpdateProduct(id, dto);
            return Results.Text("Product updated");
        });

        app.MapPatch("/updateAvailability", async (ProductService productService, string name, bool value) =>
        {
            await productService.UpdateAvailability(name, value);
            return Results.Text("Availability updated");
        });

        app.MapGet("/getAllProducts", async (ProductService productService) =>
            await productService.GetProducts());

        app.MapGet("/getProductByName", async (ProductService productService, string name) =>
            await productService.GetProductByName(name));

        app.MapDelete("/removeProduct", async (ProductService productService, string name) =>
        {
            await productService.RemoveProduct(name);
            return Results.Text("Product removed");
        });

        return app;
    }
}