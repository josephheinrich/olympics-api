using AutoMapper;
using Olympics.Api.Dtos;
using Olympics.Api.Models;

namespace Olympics.Api.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventDto>();
        // If later you add Create/Update DTOs:
        // CreateMap<EventCreateDto, Event>();
        // CreateMap<EventUpdateDto, Event>();
    }
}
