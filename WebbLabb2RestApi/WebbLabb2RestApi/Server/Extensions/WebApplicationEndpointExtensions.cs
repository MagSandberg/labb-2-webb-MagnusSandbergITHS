using WebbLabb2RestApi.Server.Services;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.Server.Extensions;

public static class WebApplicationEndpointExtensions
{
    public static WebApplication MapMongoDbEndpoints(this WebApplication app)
    {
        app.MapPost("/createProduct", async (ProductService productService, ProductDto productDto) =>
            await productService.AddProduct(productDto));

        //Ändra till Put och ta in en Dto
        app.MapPatch("/updateProduct", async (ProductService productService, string name, string value) =>
            await productService.UpdateProduct(name, value));

        app.MapPatch("/updateAvailability", async (ProductService productService, string name, bool value) =>
            await productService.UpdateAvailability(name, value));

        app.MapGet("/getAllProducts", async (ProductService productService) =>
            await productService.GetProducts());

        app.MapGet("/getProductByName", async (ProductService productService, string name) =>
            await productService.GetProductByName(name));

        app.MapDelete("/removeProduct", async (ProductService productService, string name) =>
            await productService.RemoveProduct(name));

        return app;
    }
}