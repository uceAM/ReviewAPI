using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ReviewAPI.Dto;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            _pokemonRepository = pokemonRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId) 
        {
            if (!_pokemonRepository.IsPokemonExist(pokeId))
            {
                return NotFound();
            }

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        //[ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRatings(int pokeId)
        {
            if (!_pokemonRepository.IsPokemonExist(pokeId))
            {
                return NotFound();
            }
            decimal rating = _pokemonRepository.GetPokemonRatings(pokeId);
            //var pokemonName = _pokemonRepository.GetPokemon(pokeId).Name;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rating);
            //return Ok($"{pokemonName}'s rating is {rating}");
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] PokemonDto pokemonCreate)
        {
            if(pokemonCreate == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var PokemonMap = _mapper.Map<Pokemon>(pokemonCreate);
            if(!_pokemonRepository.CreatePokemon(ownerId, catId, PokemonMap))
            {
                ModelState.AddModelError("", "internal error occured");
                return StatusCode(500, ModelState);
            }
            return Ok($"Successfully created {PokemonMap.Name}");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePokemon([Required] int pokeId, [FromBody] PokemonDto pokemon)
        {
            if(pokemon == null  || !ModelState.IsValid || pokemon.Id != pokeId)
            {
                return BadRequest(ModelState);
            }
            if (!_pokemonRepository.IsPokemonExist(pokeId))
            {
                return NotFound();
            }
            var pokemonMap = _mapper.Map<Pokemon>(pokemon);
            if(!_pokemonRepository.UpdatePokemon(pokeId,pokemonMap))
            {
                ModelState.AddModelError("", "internal server error");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
