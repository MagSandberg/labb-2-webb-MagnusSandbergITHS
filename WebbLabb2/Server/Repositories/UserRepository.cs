using Microsoft.AspNetCore.Identity;

namespace WebbLabb2.Server.Repositories;

public class UserRepository
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserRepository(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityUser> GetUserAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IList<string>> GetUserRolesAsync(IdentityUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task AssignAdminRole(UserManager<IdentityUser> userManager, string email)
    {
        var admin = await userManager.FindByEmailAsync(email);
        await userManager.AddToRoleAsync(admin, "admin");
    }

    public async Task AssignUserRole(UserManager<IdentityUser> userManager, string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        await userManager.AddToRoleAsync(user, "user");
    }
}