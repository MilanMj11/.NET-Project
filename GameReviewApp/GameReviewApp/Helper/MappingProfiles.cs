using AutoMapper;
using GameReviewApp.Dto;
using GameReviewApp.Models;

namespace GameReviewApp.Helper
{
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<Game, GameDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Company, CompanyDto>();
        }
    }
}
