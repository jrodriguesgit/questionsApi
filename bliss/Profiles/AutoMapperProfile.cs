using AutoMapper;
using Models;
using Shared.ResponsesDtos;

namespace Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ResponseQuestionDto, Question>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.image_url))
                    .ForMember(dest => dest.ThumbUrl, opt => opt.MapFrom(src => src.thumb_url))
                    .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.published_at))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.question))
                    .ForMember(dest => dest.Choices, opt => opt.MapFrom(src => src.choices))
                    .ReverseMap();
            ;
            CreateMap<ResponseChoiceDto, PossibleAnswer>()
                    .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.choice))
                    .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.votes))
                    .ReverseMap();
        }
    }
}
