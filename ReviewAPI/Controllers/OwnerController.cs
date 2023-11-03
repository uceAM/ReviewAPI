using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewAPI.Dto;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerRepository ownerRepository, IMapper mapper)
        {
           _ownerRepository = ownerRepository;
           _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type =typeof(IEnumerable<Owner>))]
        public IActionResult GetOwners()
        {
            var owners = _mapper.Map<List<OwnerDto>>(_ownerRepository.GetOwners());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owners);
        }

        [HttpGet("{ownerId}")]
        [ProducesResponseType(200,Type =typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwner(int ownerId)
        {
            if (!_ownerRepository.IsOwnerExist(ownerId))
            {
                return NotFound();
            }
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwner(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }
        [HttpGet("pokemon/{pokeId}")]
        [ProducesResponseType(200, Type =typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerByPokemonId(int pokeId)
        {
            if (!_ownerRepository.IsPokemonExist(pokeId))
            {
                return NotFound();
            }
            var owner = _mapper.Map<OwnerDto>(_ownerRepository.GetOwnerByPokemonId(pokeId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(owner);
        }

        [HttpGet("{ownerId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonsByOwnerId(int ownerId)
        {
            if(!_ownerRepository.IsOwnerExist(ownerId))
            {
                return NotFound();
            }
            var pokemons = _mapper.Map<List<PokemonDto>>(_ownerRepository.GetPokemonsByOwnerId(ownerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOwner([FromQuery] int countryId, [FromBody] OwnerDto ownerCreate)
        {
            if(ownerCreate == null)
            {
                return BadRequest(ModelState);
            }
            //var existOwners = _ownerRepository.GetOwners()
            //    .Where(o => o.)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ownerMap = _mapper.Map<Owner>(ownerCreate);
            if(!_ownerRepository.CreateOwner(countryId,ownerMap))
            {
                ModelState.AddModelError("", "internal server error");
                return StatusCode(500, ModelState);
            }
            return Ok($"Successfully created {ownerMap.FirstName} {ownerMap.LastName} as an owner");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateOwner( [Required]int ownerId, [FromBody]OwnerDto owner)
        {
            if( owner == null || !ModelState.IsValid || owner.Id != ownerId)
            {
                return BadRequest(ModelState);
            }
            if (!_ownerRepository.IsOwnerExist(ownerId))
            {
                return NotFound();
            }
            var ownerMap = _mapper.Map<Owner>(owner);
            if (!_ownerRepository.UpdateOwner(ownerId, ownerMap)) {
                ModelState.AddModelError("", "internal server error");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{ownerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteOwner(int ownerId)
        {
            if (!_ownerRepository.IsOwnerExist(ownerId))
            {
                return NotFound();
            }
            var ownerDelete = _ownerRepository.GetOwner(ownerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_ownerRepository.DeleteOwner(ownerDelete))
            {
                ModelState.AddModelError("", ("internal server error"));
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
