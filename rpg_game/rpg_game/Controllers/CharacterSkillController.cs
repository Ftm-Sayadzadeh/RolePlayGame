using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rpg_game.Dtos.ChracterSkill;
using rpg_game.Services.CharacterSkillService;

namespace rpg_game.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CharacterSkillController : ControllerBase
{
    private readonly ICharacterSkillService _characterSkillService;
    public CharacterSkillController(ICharacterSkillService characterSkillService)
    {
        _characterSkillService = characterSkillService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        return Ok(await _characterSkillService.AddCharacterSkill(newCharacterSkill));
    }
    
}