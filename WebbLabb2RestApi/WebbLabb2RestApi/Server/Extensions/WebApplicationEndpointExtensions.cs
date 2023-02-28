using System;
using WebbLabb2RestApi.Server.Services;
using WebbLabb2RestApi.Shared.DTOs;

namespace WebbLabb2RestApi.Server.Extensions;

public static class WebApplicationEndpointExtensions
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

    public static WebApplication MapMongoDbOrderEndpoints(this WebApplication app)
    {
        app.MapPost("/createOrder", async (OrderService orderService, OrderDto dto) =>
            await orderService.CreateOrder(dto));

        app.MapPatch("/updateOrder", async (OrderService orderService, string id, OrderDto dto) =>
            await orderService.UpdateOrder(id, dto));

        app.MapGet("/getOrders", async (OrderService orderService) =>
            await orderService.GetOrders());

        app.MapDelete("/removeOrder", async (OrderService orderService, string id) =>
            await orderService.RemoveOrder(id));

        return app;
    }

    public static WebApplication MapSqlDbEndpoints(this WebApplication app)
    {
        app.MapPost("/createUser", async (CustomerService customerService, CustomerDto dto) => 
            await customerService.AddUser(dto));

        app.MapGet("/getAllUsers", async (CustomerService customerService) =>
            await customerService.GetUsers());

        app.MapGet("/getUser", async (CustomerService customerService, Guid id) =>
            await customerService.GetUser(id));

        app.MapGet("/getUserByEmail", async (CustomerService customerService, string email) =>
            await customerService.GetUserByEmail(email));

        app.MapPatch("/updateUser", async (CustomerService customerService, Guid id, CustomerDto dto) =>
            await customerService.UpdateUser(id, dto));

        app.MapDelete("/removeUser", async (CustomerService customerService, Guid id) =>
            await customerService.RemoveUser(id));

        return app;
    }
}