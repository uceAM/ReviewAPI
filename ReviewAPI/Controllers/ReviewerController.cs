using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReviewAPI.Dto;
using ReviewAPI.Interfaces;
using ReviewAPI.Models;
using ReviewAPI.Repositories;
using System.Reflection.Metadata.Ecma335;

namespace ReviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.IsReviewerExist(reviewerId))
            {
                return NotFound();
            }
            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewerId(int reviewerId)
        {
            if (!_reviewerRepository.IsReviewerExist(reviewerId))
            {
                return NotFound(); 
            }
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewerId(reviewerId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult createReviewer(ReviewerDto reviewerCreate)
        {
            if (reviewerCreate == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewerMap = _mapper.Map<Reviewer>(reviewerCreate);
            if(!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "internal server error");
                return StatusCode(500, ModelState);
            }
            return Ok($"Successfully created {reviewerMap.FirstName} {reviewerMap.LastName} as a new reviewer");
        }
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateReviewer(int rvrId, ReviewerDto reviewer)
        {
            if( reviewer == null || !ModelState.IsValid || reviewer.Id != rvrId)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.IsReviewerExist(rvrId))
            {
                return NotFound();
            }
            var reviewerMap = _mapper.Map<Reviewer>(reviewer);
            if (!_reviewerRepository.UpdateReviewer(rvrId, reviewerMap))
            {
                ModelState.AddModelError("", "internal server error");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{reviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteReviewer(int reviewerId)
        {
            if (!_reviewerRepository. IsReviewerExist(reviewerId))
            {
                return NotFound();
            }
            var reviewerDelete = _reviewerRepository.GetReviewer(reviewerId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.DeleteReviewer(reviewerDelete))
            {
                ModelState.AddModelError("", ("internal server error"));
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
