using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rpg_game.Dtos.Weapon;
using rpg_game.Services.WeaponService;

namespace rpg_game.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeaponController : ControllerBase
{
    private readonly IWeaponService _weaponService;

    public WeaponController(IWeaponService weaponService)
    {
        _weaponService = weaponService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddWeapon(AddWeaponDto newWeapon)
    {
        return Ok(await _weaponService.AddWeapon(newWeapon));
    }
}