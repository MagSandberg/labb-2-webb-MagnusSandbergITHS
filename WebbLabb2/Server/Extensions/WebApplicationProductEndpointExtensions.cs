using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationProductEndpointExtensions
{
    public static WebApplication MapMongoDbProductEndpoints(this WebApplication app)
    {
        app.MapPost("/createProduct", async (ProductService productService, ProductDto dto) =>
        {
            var result = await productService.AddProduct(dto);

            return result ? Results.Ok("Product successfully added") 
                : Results.BadRequest("Product name already exists. Change the name to add the product.");
        });

        //Skapa get med ID
        app.MapGet("/getProductById", async (ProductService productService, string id) =>
        {
            return await productService.GetProductById(id);
        });

        app.MapGet("/getAllProducts", async (ProductService productService) =>
            await productService.GetProducts());

        app.MapGet("/getProductByName", async (ProductService productService, string name) =>
            await productService.GetProductByName(name));

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

        app.MapDelete("/removeProduct", async (ProductService productService, string name) =>
        {
            await productService.RemoveProduct(name);
            return Results.Text("Product removed");
        });

        return app;
    }
}