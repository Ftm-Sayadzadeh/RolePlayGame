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

    public async Task<ServiceResponse<string>> Login(string username, string password)
    {
        ServiceResponse<string> response = new ServiceResponse<string>();
        User user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
        // user not found state 
        if (user == null)
        {
            response.Success = false;
            response.Massage = "User not found.";
        }
        else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            response.Success = false;
            response.Massage = "Wrong password.";
        }
        else
        {
            response.Data = user.Id.ToString();
        }

        return response;
    }
    
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
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