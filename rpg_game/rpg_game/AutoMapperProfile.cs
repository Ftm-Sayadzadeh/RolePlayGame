using AutoMapper;
using rpg_game.Dtos.Character;
using rpg_game.Dtos.Skill;
using rpg_game.Dtos.Weapon;
using rpg_game.Models;

namespace rpg_game
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>()
                .ForMember(dto => dto.Skills,
                    c => c.MapFrom(ch => ch.CharacterSkills.Select(cs => cs.Skill)));
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}