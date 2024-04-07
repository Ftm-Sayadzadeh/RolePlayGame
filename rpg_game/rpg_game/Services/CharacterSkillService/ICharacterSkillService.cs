using rpg_game.Dtos.Character;
using rpg_game.Dtos.ChracterSkill;
using rpg_game.Migrations;
using rpg_game.Models;

namespace rpg_game.Services.CharacterSkillService;

public interface ICharacterSkillService
{
    Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
}