using Microsoft.EntityFrameworkCore;
using WebbLabb2.DataAccess.Sql.Contexts;
using WebbLabb2.DataAccess.Sql.Models;
using WebbLabb2.DataAccess.Sql.Repositories;
using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationCustomerEndpointExtensions
{
    public static WebApplication MapSqlDbCustomerEndpoints(this WebApplication app)
    {

        app.MapPost("/createCustomer", async (CustomerService customerService, CustomerDto dto) =>
        {
            await customerService.AddCustomer(dto);
            return Results.Text("Customer successfully added");
        });

        app.MapGet("/getAllCustomers", async (CustomerService customerService) =>
            await customerService.GetCustomers());

        app.MapGet("/getCustomer", async (CustomerService customerService, Guid id) =>
            await customerService.GetCustomer(id));

        app.MapGet("/getCustomerByEmail", async (CustomerService customerService, string email) =>
            await customerService.GetCustomerByEmail(email));

        app.MapPatch("/updateCustomer", async (CustomerService customerService, Guid id, CustomerDto dto) =>
        {
            await customerService.UpdateCustomer(id, dto);
            return Results.Text("Update complete");
        });

        app.MapDelete("/removeCustomer", async (CustomerService customerService, Guid id) =>
        {
            await customerService.RemoveCustomer(id);
            return Results.Text("Customer removed");
        });

        return app;
    }
}