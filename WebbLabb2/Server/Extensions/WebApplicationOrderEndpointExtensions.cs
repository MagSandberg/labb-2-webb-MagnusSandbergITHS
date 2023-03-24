using MongoDB.Bson;
using WebbLabb2.Client;
using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationOrderEndpointExtensions
{
    public static WebApplication MapMongoDbOrderEndpoints(this WebApplication app)
    {
        app.MapPost("/createOrder", async (OrderService orderService, OrderDto dto) =>
        {
            var result = await orderService.CreateOrder(dto);

            return result ? Results.Ok("Order successfully added") :
                Results.BadRequest("Email is not registered.");
        });

        app.MapPatch("/updateOrder", async (OrderService orderService, string id, OrderDto dto) =>
        {
            var result = await orderService.UpdateOrder(id, dto);
            return result == false ? Results.NotFound("Order ID doesn't exist") : Results.Ok("Order updated");
        });

        app.MapGet("/getOrder/{id}", async (OrderService orderService, string id) =>
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                var result = await orderService.GetOrder(objectId.ToString());
                return result == null ? Results.NotFound("Product ID doesn't exist.") : Results.Ok(result);
            }

            return Results.BadRequest("Not a valid ID.");
        });

        app.MapGet("/getOrders", async (OrderService orderService) =>
            await orderService.GetOrders());

        app.MapDelete("/removeOrder/{id}", async (OrderService orderService, string id) =>
        {
            var result = await orderService.RemoveOrder(id);
            return result == false ? Results.NotFound("Order ID doesn't exist") : Results.Ok("Order removed");
        });

        app.MapDelete("/removePlaceholderOrder/{email}", async (OrderService orderService, string email) =>
        {
            var result = await orderService.RemovePlaceHolderOrder(email);
            return result == false ? Results.NotFound("Order Email doesn't exist") : Results.Ok("Order removed");
        });

        return app;
    }
}