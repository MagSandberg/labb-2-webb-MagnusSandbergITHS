﻿using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationCustomerEndpointExtensions
{
    public static WebApplication MapSqlDbCustomerEndpoints(this WebApplication app)
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