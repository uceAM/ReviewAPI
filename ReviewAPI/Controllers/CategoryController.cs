using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReviewAPI.Dto;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCatgories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
        [HttpGet("{catId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int catId)
        {
            if (!_categoryRepository.IsCategoryExist(catId))
            {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(catId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }
        [HttpGet("{catId}/pokemon")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int catId)
        {
            if (!_categoryRepository.IsCategoryExist(catId))
            {
                return NotFound();
            }
            var pokemons = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonByCategoryId(catId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(pokemons);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper())
                .FirstOrDefault();//Category should ideally be a table with unique constraint on Name
            if (category != null)
            {
                ModelState.AddModelError("", "Category with this category name already exists.");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Internal Server Error");
                return StatusCode(500, ModelState);
            }
            return Ok($"Successfully created {categoryMap.Name} Category");
        }
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory([Required] int catId, [FromBody] CategoryDto category)
        {
            if (category == null || !ModelState.IsValid || category.Id != catId)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.IsCategoryExist(catId))
            {
                return NotFound();
            }
            var categoryMap = _mapper.Map<Category>(category);
            if (!_categoryRepository.UpdateCategory(catId, categoryMap))
            {
                ModelState.AddModelError("", "internal server error");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{catId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteCategory(int catId)
        {
            if (!_categoryRepository.IsCategoryExist(catId))
            {
                return NotFound();
            }
            var categoryDelete = _categoryRepository.GetCategory(catId);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.DeleteCategory(categoryDelete))
            {
                ModelState.AddModelError("", ("internal server error"));
                    return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
