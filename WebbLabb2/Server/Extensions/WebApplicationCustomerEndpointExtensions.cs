using WebbLabb2.Server.Services;
using WebbLabb2.Shared.DTOs;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationCustomerEndpointExtensions
{
    private static readonly HttpClient? _httpClient = new ();
    public static WebApplication MapSqlDbCustomerEndpoints(this WebApplication app)
    {
        app.MapPost("/createCustomer", async (CustomerService customerService, CustomerDto dto) =>
        {
            var response = await _httpClient.GetAsync("https://localhost:7062/createCustomer");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await customerService.AddCustomer(dto);
                return Results.Text("Customer successfully added");
            }

            return Results.Text("Something went wrong");
        });

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