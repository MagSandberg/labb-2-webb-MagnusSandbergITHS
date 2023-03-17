using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationCustomerEndpointExtensions
{
    public static WebApplication MapSqlDbCustomerEndpoints(this WebApplication app)
    {

        app.MapPost("/createCustomer", async (CustomerService customerService, CustomerDto dto) =>
        {
            var result = await customerService.AddCustomer(dto);
            return result ? Results.Ok("Customer successfully added") :
                Results.BadRequest("Email is already in use. Please choose a different address.");
        });

        app.MapGet("/getCustomer", async (CustomerService customerService, string id) =>
        {
            //TODO Bryt ut till en metod som kollar guid eller skapa en helper-class med alla checkar

            if (Guid.TryParse(id, out var guid))
            {
                var result = await customerService.GetCustomer(guid);
                return result == null ? Results.NotFound("ID doesn't exist.") : Results.Ok(result);
            }

            return Results.BadRequest("Not a valid ID.");
        });

        app.MapGet("/getCustomerByEmail/{email}", async (CustomerService customerService, string email) =>
        {
            var result = await customerService.GetCustomerByEmail(email);
            return result == null ? Results.NotFound("Email doesn't exist.") : Results.Ok(result);
        });

        app.MapGet("/getAllCustomers", async (CustomerService customerService) =>
        {
            var result = await customerService.GetCustomers();
            return Results.Ok(result);
        });

        app.MapPatch("/updateCustomer", async (CustomerService customerService, Guid id, CustomerDto dto) =>
        {
            var result = await customerService.UpdateCustomer(id, dto);
            return result == false ? Results.NotFound("ID doesn't exist.") : Results.Ok("Update complete");
        });

        app.MapDelete("/removeCustomer/{id}", async (CustomerService customerService, Guid id) =>
        {
            var result = await customerService.RemoveCustomer(id);
            return result == false ? Results.NotFound("ID doesn't exist.") : Results.Ok("Customer removed");
        });

        return app;
    }
}