using AutoMapper;
using Olympics.Api.Dtos;
using Olympics.Api.Models;

namespace Olympics.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Athlete, AthleteDto>()
            .ForCtorParam(nameof(AthleteDto.Id), opt => opt.MapFrom(a => a.AthleteId))
            .ForCtorParam(nameof(AthleteDto.Name), opt => opt.MapFrom(a => a.AthleteName))
            .ForCtorParam(nameof(AthleteDto.Sex), opt => opt.MapFrom(a => a.Sex.ToString()));

        CreateMap<Result, ResultDto>()
            .ForCtorParam(nameof(ResultDto.AthleteId), opt => opt.MapFrom(r => r.AthleteId))
            .ForCtorParam(nameof(ResultDto.AthleteName), opt => opt.MapFrom(r => r.Athlete.AthleteName))
            .ForCtorParam(nameof(ResultDto.Noc), opt => opt.MapFrom(r => r.Team.NocId))
            .ForCtorParam(nameof(ResultDto.Team), opt => opt.MapFrom(r => r.Team.TeamName))
            .ForCtorParam(nameof(ResultDto.Sport), opt => opt.MapFrom(r => r.Event.Sport.SportName))
            .ForCtorParam(nameof(ResultDto.Event), opt => opt.MapFrom(r => r.Event.EventName))
            .ForCtorParam(nameof(ResultDto.Year), opt => opt.MapFrom(r => r.Game.Year))
            .ForCtorParam(nameof(ResultDto.Season), opt => opt.MapFrom(r => r.Game.Season))
            .ForCtorParam(nameof(ResultDto.City), opt => opt.MapFrom(r => r.Game.City))
            .ForCtorParam(nameof(ResultDto.Medal), opt => opt.MapFrom(r => r.Medal));
    }
}
