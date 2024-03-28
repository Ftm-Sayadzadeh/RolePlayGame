using Microsoft.EntityFrameworkCore;
using rpg_game.Models;

namespace rpg_game.Data;

public class AuthRepository : IAuthRepository
{

    private readonly DataContext _context;

    public AuthRepository(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<int>> Register(User user, string password)
    {
        // check user existence
        ServiceResponse<int> response = new ServiceResponse<int>();
        if (await UserExists(user.Username))
        {
            response.Success = false;
            response.Massage = "User already exists.";
            return response;
        }
        
        // call CreatePasswordHash and set hash and salt
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        /*
           add this new user, save all changes, 
           create the ServiceResponse
           and send the new Id of the user back
         */
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        response = new ServiceResponse<int>();
        response.Data = user.Id;
        return response;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public Task<ServiceResponse<string>> Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UserExists(string username)
    {
        if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
        {
            return true;
        }
        return false;
    }
}