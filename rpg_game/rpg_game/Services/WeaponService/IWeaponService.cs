using rpg_game.Dtos.Character;
using rpg_game.Dtos.Weapon;
using rpg_game.Models;

namespace rpg_game.Services.WeaponService;

public interface IWeaponService
{
    Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
}