using MongoDB.Bson;
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

            return result ? Results.Ok("Product successfully added.") 
                : Results.BadRequest("Product name already exists. Change the name to add the product.");
        });
        
        app.MapGet("/getProductById", async (ProductService productService, string id) =>
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                var result = await productService.GetProductById(objectId.ToString());
                return result == null ? Results.NotFound("Product ID doesn't exist.") : Results.Ok(result);
            }

            return Results.BadRequest("Not a valid ID.");
        });

        app.MapGet("/getProductByName", async (ProductService productService, string name) =>
        {
                var result = await productService.GetProductByName(name);
                return result == null ? Results.NotFound("Product name doesn't exist.") : Results.Ok(result);
        });

        app.MapGet("/getAllProducts", async (ProductService productService) =>
        {
            return Results.Ok(await productService.GetProducts());
        });

        app.MapPatch("/updateProduct", async (ProductService productService, string id, ProductDto dto) =>
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                var result = await productService.UpdateProduct(objectId.ToString(), dto);
                return result == false ? Results.NotFound("Product ID doesn't exist.") : Results.Ok("Product updated.");
            }

            return Results.BadRequest("Not a valid ID.");
        });

        app.MapPatch("/updateAvailability", async (ProductService productService, string name, bool value) =>
        {
            var result = await productService.UpdateAvailability(name, value);
            return result == false ? Results.NotFound("Product name doesn't exist.") : Results.Ok("Availability updated.");
        });

        app.MapDelete("/removeProduct", async (ProductService productService, string id) =>
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                var result = await productService.RemoveProduct(objectId.ToString());
                return result == false ? Results.NotFound("Product ID doesn't exist.") : Results.Ok("Product removed.");
            }

            return Results.BadRequest("Not a valid ID.");
        });

        return app;
    }
}