using AutoMapper;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.Models.DTOs
{
    public class TVSeriesProfile : Profile
    {
        public TVSeriesProfile()
        {
            #region CastMember maps
            CreateMap<CastMemberCreateDto, CastMember>();
            CreateMap<CastMember, CastMemberReadDto>();
            CreateMap<CastMember, CastMemberUpdateDto>();
            CreateMap<CastMemberUpdateDto, CastMember>();
            #endregion

            #region Episode maps
            CreateMap<EpisodeCreateDto, Episode>();
            CreateMap<Episode, EpisodeReadDto>()
                .ForMember(epDto => epDto.CastMembers, opt => opt.MapFrom(ep => ep.CastMembers.Select(cm => cm.CastMember).ToList()));
            #endregion

            #region Genre maps
            CreateMap<GenreCreateDto, Genre>();
            CreateMap<Genre, GenreReadDto>();
            CreateMap<Genre, GenreUpdateDto>();
            CreateMap<GenreUpdateDto, Genre>();
            #endregion

            #region Season maps
            CreateMap<SeasonCreateDto, Season>();
            CreateMap<Season, SeasonReadDto>();
            #endregion

            #region Serie maps
            CreateMap<SerieCreateDto, Serie>();
            CreateMap<Serie, SerieReadDto>();
            #endregion
        }
    }
}
