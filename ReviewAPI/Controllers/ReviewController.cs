using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewAPI.Dto;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;

namespace ReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        public IActionResult GetReviews()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.IsReviewExist(reviewId))
            {
                return NotFound();
            }
            var reviews = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpGet("{pokeId}/review")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByPokemonId(int pokeId)
        {
            if (!_reviewRepository.IsPokemonExist(pokeId))
            {
                return NotFound();
            }
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsByPokemonId(pokeId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId,[FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if(reviewCreate == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewMap = _mapper.Map<Review>(reviewCreate);
            if (!_reviewRepository.CreateReview(reviewerId, pokeId, reviewMap))
            {
                ModelState.AddModelError("", "internal server error");
                return StatusCode(500, ModelState);
            }
            return Ok($"Successfully created a new review for {reviewMap.Pokemon.Name} by {reviewMap.Reviewer.FirstName} {reviewMap.Reviewer.LastName}");
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateReview(int reviewId, ReviewDto review)
        {
            if(review == null || !ModelState.IsValid || review.Id != reviewId)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.IsReviewExist(reviewId))
            {
                return NotFound();
            }
            var reviewMap = _mapper.Map<Review>(review);
            if (!_reviewRepository.UpdateReview(reviewId, reviewMap)){
                ModelState.AddModelError("", "internal server error");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.IsReviewExist(reviewId))
            {
                return NotFound();
            }
            var reviewDelete = _reviewRepository.GetReview(reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.DeleteReview(reviewDelete))
            {
                ModelState.AddModelError("", ("internal server error"));
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
