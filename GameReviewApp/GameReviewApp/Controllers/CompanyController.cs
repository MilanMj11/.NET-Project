using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using GameReviewApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace GameReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepository;

        public CompanyController(ICompanyRepository companyRepository,IMapper mapper, IGameRepository gameRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public IActionResult GetCompanies()
        {
            var companies = _mapper.Map<List<CompanyDto>>(_companyRepository.GetCompanies());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(companies);
        }

        [HttpGet("{companyId}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompany(int companyId)
        {
            if (!_companyRepository.CompanyExists(companyId))
                return NotFound();

            var company = _mapper.Map<CompanyDto>(_companyRepository.GetCompany(companyId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(company);
        }

        [HttpGet("game/{companyId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Game>))]
        [ProducesResponseType(400)]
        public IActionResult GetGamesByCompanyId(int companyId)
        {
            if (!_companyRepository.CompanyExists(companyId))
                return NotFound();

            var games = _mapper.Map<List<GameDto>>(_companyRepository.GetGamesByCompanyId(companyId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(games);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromBody] CompanyDto companyCreate)
        {
            if (companyCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var company = _mapper.Map<Company>(companyCreate);

            if (!_companyRepository.CreateCompany(company))
                return BadRequest("Cannot create company.");

            return Ok("Company successfully created.");
        }

        [HttpPut("{companyId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompany(int companyId,[FromBody] CompanyDto companyUpdate)
        {
            if(companyUpdate == null)
                return BadRequest(ModelState);

            if (!_companyRepository.CompanyExists(companyId))
                return BadRequest("Company does not exist.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var company = _mapper.Map<Company>(companyUpdate);
            company.Id = companyId;

            if (!_companyRepository.UpdateCompany(companyId,company))
            {
                ModelState.AddModelError("", "Error updating company.");
                return StatusCode(500, ModelState);
            }


            return Ok("Company successfully updated.");
        }

        [HttpDelete("{companyId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCompany(int companyId)
        {
            if (!_companyRepository.CompanyExists(companyId))
                return NotFound();

            var games = _companyRepository.GetGamesByCompanyId(companyId).ToList();
            var company = _companyRepository.GetCompany(companyId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_gameRepository.DeleteGames(games))
            {
                ModelState.AddModelError("", "Error deleting games.");
                return StatusCode(500, ModelState);
            }

            if (!_companyRepository.DeleteCompany(company))
            {
                ModelState.AddModelError("", "Error deleting company.");
                return StatusCode(500, ModelState);
            }

            return Ok("Company successfully deleted.");
        }


    }
}
