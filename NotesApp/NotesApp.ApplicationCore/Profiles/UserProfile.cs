using AutoMapper;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.ApplicationCore.Models;
using NotesApp.ApplicationCore.Users.Commands;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<RegisterUserDto, CreateUserCommand>();
        CreateMap<JwtToken, SetUserTokenCommand>()
            .ForMember(dest => dest.RefreshToken,
                opt =>
                    opt.MapFrom(src => src.RefreshToken))
            .ForMember(dest => dest.TokenCreated,
                opt =>
                    opt.MapFrom(src => src.Created))
            .ForMember(dest => dest.TokenExpires,
                opt =>
                    opt.MapFrom(src => src.Expires));
    }
}