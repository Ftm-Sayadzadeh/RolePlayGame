using Microsoft.AspNetCore.Mvc;
using rpg_game.Data;
using rpg_game.Dtos;
using rpg_game.Models;

namespace rpg_game.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;
    public AuthController(IAuthRepository authRepository)
    {
        _authRepo = authRepository;
    }
    
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        ServiceResponse<int> response = await _authRepo.Register(
            new User { Username = request.Username }, request.Password);
        if(!response.Success) {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDto request)
    {
        ServiceResponse<string> response = await _authRepo.Login(
            request.Username, request.Password);
        if(!response.Success) {
            return BadRequest(response);
        }
        return Ok(response);
    }
}