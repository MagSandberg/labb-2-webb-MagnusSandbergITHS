using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationOrderEndpointExtensions
{
    public static WebApplication MapMongoDbOrderEndpoints(this WebApplication app)
    {
        app.MapPost("/createOrder", async (OrderService orderService, OrderDto dto) =>
            await orderService.CreateOrder(dto));

        app.MapPatch("/updateOrder", async (OrderService orderService, string id, OrderDto dto) =>
            await orderService.UpdateOrder(id, dto));

        app.MapGet("/getOrder", async (OrderService orderService, string id) =>
            await orderService.GetOrder(id));

        app.MapGet("/getOrders", async (OrderService orderService) =>
            await orderService.GetOrders());

        app.MapDelete("/removeOrder", async (OrderService orderService, string id) =>
            await orderService.RemoveOrder(id));

        return app;
    }
}