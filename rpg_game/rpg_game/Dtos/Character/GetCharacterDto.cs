using rpg_game.Dtos.Skill;
using rpg_game.Dtos.Weapon;
using rpg_game.Models;

namespace rpg_game.Dtos.Character;

public class GetCharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "Fredo";
    public int HitPoints { get; set; } = 10;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Knight;
    public GetWeaponDto Weapon { get; set; }
    public List<GetSkillDto> Skills { get; set; }
}