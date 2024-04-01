using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rpg_game.Dtos.Character;
using rpg_game.Models;
using rpg_game.Services.CharacterService;

namespace rpg_game.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CharacterController : ControllerBase
{
    
    private readonly ICharacterService _characterService;
    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }
    private static Character knight = new Character();
    
    
    // [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> Get()
    { 
        return Ok(await _characterService.GetAllCharacters());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
    {
        return Ok(await _characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        // return a 404 Not Found response when id does not exist in list
        ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updatedCharacter);
        if (response.Data == null)
        {
            return NotFound(response);
        }
        
        return Ok(await _characterService.UpdateCharacter(updatedCharacter));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // return a 404 Not Found response when id does not exist in list
        ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(id);
        if (response.Data == null)
        {
            return NotFound(response);
        }

        return Ok(response);
    }
}