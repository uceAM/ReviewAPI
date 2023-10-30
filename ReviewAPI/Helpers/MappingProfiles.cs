using AutoMapper;
using ReviewAPI.Dto;
using ReviewAPI.Models;

namespace ReviewAPI.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
        }
    }
}
