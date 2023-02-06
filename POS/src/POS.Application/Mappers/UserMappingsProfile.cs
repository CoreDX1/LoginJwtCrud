using AutoMapper;
using POS.Application.Dto.Request;
using POS.Application.Dto.Response;
using POS.Domain.Entities;

namespace POS.Application.Mappers;

public class UserMappingsProfile : Profile
{
    public UserMappingsProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<User, UserSelectResponseDto>();
        CreateMap<UserRequestDto, User>();
        CreateMap<TokenRequestDto, User>();
    }
}
