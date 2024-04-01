using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rpg_game.Data;
using rpg_game.Dtos.Character;
using rpg_game.Models;

namespace rpg_game.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CharacterService(IMapper mapper , DataContext context , IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    
    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    // why list ?????
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        // map newCharacter into Character
        Character character = _mapper.Map<Character>(newCharacter);
        // grab the user obj from database
        character.User =  await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
        // add the RPG character to the database and use AddAsync
        await _context.Characters.AddAsync(character);
        await _context.SaveChangesAsync();
        // map every char of the list into GCD
        // return this user characters ???
        serviceResponse.Data = (_context.Characters.Where(c => c.User.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
        return serviceResponse;
    }

    

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        // get char from database 
        List<Character> dbCharacters = await _context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
        // map every char of the list into GCD
        serviceResponse.Data = (dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
        Character dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
        serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
        return serviceResponse;
    }
    
    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
        
        // handle System.NullReferenceException
        try
        {
            // find this character in list and then update its fields
            Character character = await _context.Characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            if (character.User.Id == GetUserId())
            {
                // override prop of this character
                character.Name = updatedCharacter.Name;
                character.Class = updatedCharacter.Class;
                character.Defense = updatedCharacter.Defense;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Strength = updatedCharacter.Strength;
                // update and save changes in database
                _context.Characters.Update(character);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                serviceResponse.Message = "You’re character has been saved.";
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found.";
            }
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        
        // handle System.NullReferenceException
        try
        {
            // find this character in list and then delete it - save change in database
            Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            if (character != null)
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                // ????
                serviceResponse.Data = (_context.Characters.Where(c => c.User.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
                serviceResponse.Message = "You’re character has been deleted.";
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Character not found.";
            }
        }
        catch(Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        return serviceResponse;
    }
}