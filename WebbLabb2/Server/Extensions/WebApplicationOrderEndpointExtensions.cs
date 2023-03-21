using MongoDB.Bson;
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
            //TODO Bryt ut till en metod som kollar ObjectId eller skapa en helper-class med alla checkar

            if (ObjectId.TryParse(id, out var objectId))
            {
                var result = await orderService.UpdateOrder(objectId.ToString(), dto);
                return result == false ? Results.NotFound("Order ID doesn't exist") : Results.Ok("Order updated");
            }

            return Results.BadRequest("Not a valid ID.");
        });

        app.MapGet("/getOrder/{id}", async (OrderService orderService, string id) =>
        {
            var result = await orderService.GetOrder(id);
            return result == null ? Results.NotFound("Order ID doesn't exist") : Results.Ok(result);
        });

        app.MapGet("/getOrders", async (OrderService orderService) =>
            await orderService.GetOrders());

        app.MapDelete("/removeOrder/{id}", async (OrderService orderService, string id) =>
        {
            var result = await orderService.RemoveOrder(id);
            return result == false ? Results.NotFound("Order ID doesn't exist") : Results.Ok("Order removed");
        });

        return app;
    }
}