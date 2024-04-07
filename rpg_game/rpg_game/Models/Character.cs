namespace rpg_game.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = "Fredo";
    public int HitPoints { get; set; } = 10;
    public int Strength { get; set; } = 10;
    public int Defense { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Knight;
    
    // one to many relation between character and user
    public User User { get; set; }
    // one to one relation between character and user
    public Weapon Weapon { get; set; }
    // many to many relation between character and skills
    public List<CharacterSkill> CharacterSkills { get; set; }
}