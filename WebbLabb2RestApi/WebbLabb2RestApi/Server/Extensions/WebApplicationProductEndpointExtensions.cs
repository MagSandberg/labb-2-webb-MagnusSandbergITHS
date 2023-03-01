using WebbLabb2RestApi.Server.Services;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.Server.Extensions;

public static class WebApplicationProductEndpointExtensions
{
    public static WebApplication MapMongoDbProductEndpoints(this WebApplication app)
    {
        app.MapPost("/createProduct", async (ProductService productService, ProductDto dto) =>
            await productService.AddProduct(dto));

        app.MapPatch("/updateProduct", async (ProductService productService, string id, ProductDto dto) =>
            await productService.UpdateProduct(id, dto));

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