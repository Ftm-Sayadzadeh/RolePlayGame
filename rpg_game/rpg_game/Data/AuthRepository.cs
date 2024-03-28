using rpg_game.Models;

namespace rpg_game.Data;

public class AuthRepository : IAuthRepository
{
    public Task<ServiceResponse<int>> Register(User user, string password)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<string>> Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserExists(string username)
    {
        throw new NotImplementedException();
    }
}