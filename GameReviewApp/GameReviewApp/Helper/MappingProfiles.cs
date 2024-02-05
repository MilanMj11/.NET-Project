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
            CreateMap<GameDto, Game>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();

            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();

            CreateMap<Models.Profile, ProfileDto>();
            CreateMap<ProfileDto, Models.Profile>();
        }
    }
}
