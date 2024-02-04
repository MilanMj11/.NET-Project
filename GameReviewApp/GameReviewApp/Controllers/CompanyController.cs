﻿using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Interfaces;
using GameReviewApp.Models;
using GameReviewApp.Repository;
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

        public CompanyController(ICompanyRepository companyRepository,IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
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

    }
}
