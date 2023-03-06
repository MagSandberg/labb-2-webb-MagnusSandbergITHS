using Microsoft.AspNetCore.Identity;
using WebbLabb2.Server.Repositories;

namespace WebbLabb2.Server.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IdentityUser> GetUser(string email)
    {
        return await _userRepository.GetUserAsync(email);
    }

    public async Task<IList<string>> GetUserRoles(IdentityUser user)
    {
        return await _userRepository.GetUserRolesAsync(user);
    }

    public async Task AssignAdmin(UserManager<IdentityUser> userManager, string email)
    {
        await _userRepository.AssignAdminRole(userManager, email);
    }

    public async Task AssignUser(UserManager<IdentityUser> userManager, string email)
    {
        await _userRepository.AssignUserRole(userManager, email);
    }
}