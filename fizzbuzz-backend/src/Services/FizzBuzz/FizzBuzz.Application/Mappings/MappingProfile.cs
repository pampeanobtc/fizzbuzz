using FizzBuzz.Application.Features.Commands;
using FizzBuzz.Application.Models;
using FizzBuzz.Domain.Entities;

namespace FizzBuzz.Application.Mappings;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FizzBuzzResultModel, FizzBuzzProcessor>()
            .ForMember(
                opt => opt.SigningTimestamp,
                cfg => cfg.MapFrom(x => x.Signature))
            .ForMember(
                opt => opt.Items,
                cfg => cfg.MapFrom(x => x.Items))
            .ReverseMap();
    }
}
