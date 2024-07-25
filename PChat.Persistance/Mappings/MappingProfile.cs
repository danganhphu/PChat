using AutoMapper;
using PChat.Application.Features.AuthFeatures.Commands.Register;
using PChat.Domain.Entities;

namespace PChat.Persistance.Mappings;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterCommand, User>();
    }
}
