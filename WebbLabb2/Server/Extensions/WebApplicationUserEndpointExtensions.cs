using Microsoft.AspNetCore.Identity;
using WebbLabb2.Server.Services;

namespace WebbLabb2.Server.Extensions;

public static class WebApplicationUserEndpointExtensions
{
    public static WebApplication MapSqlDbUserEndpoints(this WebApplication app)
    {
        app.MapGet("/getUser", async (UserService userService, string email) =>
            await userService.GetUser(email));

        app.MapGet("/getUserRoles", async (UserService userService, IdentityUser user) =>
            await userService.GetUserRoles(user));

        app.MapPatch("/assignAdminRole", async (UserService userService, UserManager<IdentityUser> user, string email) =>
            await userService.AssignAdmin(user, email));

        app.MapPatch("/assignUserRole", async (UserService userService, UserManager<IdentityUser> user, string email) =>
            await userService.AssignUser(user, email));

        return app;
    }
}