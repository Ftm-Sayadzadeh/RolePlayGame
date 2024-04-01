using rpg_game.Dtos.Character;
using rpg_game.Models;

namespace rpg_game.Services.CharacterService;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
    Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto);
    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
}