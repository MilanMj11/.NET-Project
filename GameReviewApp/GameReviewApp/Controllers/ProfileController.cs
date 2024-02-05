using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using GameReviewApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public ProfileController(IProfileRepository profileRepository,IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Models.Profile>))]
        public IActionResult GetProfiles()
        {
            var profiles = _mapper.Map<List<ProfileDto>>(_profileRepository.GetProfiles());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profiles);
        }

        [HttpGet("{profileId}")]
        [ProducesResponseType(200, Type = typeof(Models.Profile))]
        [ProducesResponseType(400)]
        public IActionResult GetProfile(int profileId)
        {
            if (!_profileRepository.ProfileExists(profileId))
                return NotFound();

            var profile = _mapper.Map<ProfileDto>(_profileRepository.GetProfile(profileId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(profile);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProfile([FromQuery] int userId, [FromQuery] int reviewerId, [FromBody] ProfileDto profileCreate)
        {
            if (profileCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profile = _mapper.Map<Models.Profile>(profileCreate);
            profile.UserId = userId;
            profile.ReviewerId = reviewerId;

            if (!_profileRepository.CreateProfile(profile))
                return BadRequest("Cannot create profile.");

            return Ok("Profile successfully created.");
        }

        [HttpPut("{reviewId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProfile(int profileId, [FromBody] ProfileDto profileUpdate)
        {
            if (profileUpdate == null)
                return BadRequest(ModelState);

            if (!_profileRepository.ProfileExists(profileId))
                return BadRequest("Profile does not exist.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var profile = _profileRepository.GetProfile(profileId);
            profile.Name = profileUpdate.Name;
            profile.Reputation = profileUpdate.Reputation;

            if (!_profileRepository.UpdateProfile(profile))
            {
                ModelState.AddModelError("", "Error updating profile.");
                return StatusCode(500, ModelState);
            }

            return Ok("Profile successfully updated.");
        }

        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProfile(int profileId)
        {
            if (!_profileRepository.ProfileExists(profileId))
                return NotFound();

            var profile = _profileRepository.GetProfile(profileId);

            if (!_profileRepository.DeleteProfile(profile))
            {
                ModelState.AddModelError("", "Error deleting profile.");
                return StatusCode(500, ModelState);
            }

            return Ok("Profile successfully deleted.");
        }

    }
}
