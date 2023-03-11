﻿using MongoDB.Bson;
using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                return result == null ? Results.NotFound("Order ID doesn't exist") : Results.Ok("Order updated");
            }

            return Results.BadRequest("Not a valid ID.");
        });

        app.MapGet("/getOrder", async (OrderService orderService, string id) =>
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                var result = await orderService.GetOrder(objectId.ToString());
                return result == null ? Results.NotFound("Order ID doesn't exist") : Results.Ok(result);
            }

            return Results.BadRequest("Not a valid ID.");
        });

        app.MapGet("/getOrders", async (OrderService orderService) =>
            await orderService.GetOrders());

        app.MapDelete("/removeOrder", async (OrderService orderService, string id) =>
        {
            if (ObjectId.TryParse(id, out var objectId))
            {
                var result = await orderService.RemoveOrder(objectId.ToString());
                return result == false ? Results.NotFound("Order ID doesn't exist") : Results.Ok("Order removed");
            }

            return Results.BadRequest("Not a valid ID.");
        });

        return app;
    }
}