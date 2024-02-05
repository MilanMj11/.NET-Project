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
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository,IMapper mapper)
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
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int gameId, [FromQuery] int reviewerId,[FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = _mapper.Map<Review>(reviewCreate);
            review.GameId = gameId;
            review.ReviewerId = reviewerId;

            if (!_reviewRepository.CreateReview(review))
                return BadRequest("Cannot create review.");

            return Ok("Review successfully created.");
        }

        [HttpPut("{reviewId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto reviewUpdate)
        {
            if (reviewUpdate == null)
                return BadRequest(ModelState);

            if (!_reviewRepository.ReviewExists(reviewId))
                return BadRequest("Review does not exist.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var review = _reviewRepository.GetReview(reviewId);
            review.Rating = reviewUpdate.Rating;
            review.Title = reviewUpdate.Title;
            review.Text = reviewUpdate.Text;

            if (!_reviewRepository.UpdateReview(review))
            {
                ModelState.AddModelError("", "Error updating review.");
                return StatusCode(500, ModelState);
            }

            return Ok("Review successfully updated.");
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
                return NotFound();

            var review = _reviewRepository.GetReview(reviewId);

            if (!_reviewRepository.DeleteReview(review))
            {
                ModelState.AddModelError("", "Error deleting review.");
                return StatusCode(500, ModelState);
            }

            return Ok("Review successfully deleted.");
        }

    }
}
