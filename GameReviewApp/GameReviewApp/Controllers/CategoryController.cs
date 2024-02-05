using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using GameReviewApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApp.Controllers
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
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(category);
        }

        [HttpGet("game/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult GetGamesByCategoryId(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var games = _mapper.Map<List<GameDto>>(_categoryRepository.GetGamesByCategory(categoryId));

            if(!ModelState.IsValid)
                return BadRequest();

            return Ok(games);
        }
        
        [HttpPost, Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(category))
                return BadRequest("Cannot create category.");

            return Ok("Category successfully created.");
        }

        [HttpPut("{categoryId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryUpdate)
        {
            if(categoryUpdate == null)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(categoryId))
                return BadRequest("Category does not exist.");

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<Category>(categoryUpdate);
            category.Id = categoryId;

            if (!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("", "Error updating category.");
                return StatusCode(500, ModelState);
            }

            return Ok("Category successfully updated.");
        }


        [HttpDelete, Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
                return NotFound();

            var category = _categoryRepository.GetCategory(categoryId);

            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("", "Error deleting category.");
                return StatusCode(500, ModelState);
            }

            return Ok("Category successfully deleted.");
        }


    }
}
