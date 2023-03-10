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

            return result? Results.Ok("Customer successfully added") : 
                Results.BadRequest("Email is already in use. Please choose a different address.");
        });

        app.MapGet("/getCustomer", async (CustomerService customerService, string id) =>
        {
            //TODO Bryt ut till en metod som kollar guid eller en helper-class
            Guid guid;

            if (Guid.TryParse(id, out guid))
            {
                var result = await customerService.GetCustomer(guid);

                if (result == null)
                {
                    return Results.BadRequest("ID doesn't exist.");
                }

                return Results.Ok(result);
            }

            return Results.BadRequest("Not a valid ID.");
        });

        app.MapGet("/getAllCustomers", async (CustomerService customerService) =>
            await customerService.GetCustomers());

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