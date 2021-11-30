using AutoMapper;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.Models.DTOs
{
    public class TVSeriesProfile : Profile
    {
        public TVSeriesProfile()
        {
            CreateMap<CastMemberCreateDto, CastMember>();
            CreateMap<CastMember, CastMemberReadDto>();

            CreateMap<EpisodeCreateDto, Episode>();
            CreateMap<Episode, EpisodeReadDto>();

            CreateMap<GenreCreateDto, Genre>();
            CreateMap<Genre, GenreReadDto>();

            CreateMap<SeasonCreateDto, Season>();
            CreateMap<Season, SeasonReadDto>();

            CreateMap<SeasonCreateDto, Serie>();
            CreateMap<Season, SerieReadDto>();
        }
    }
}
