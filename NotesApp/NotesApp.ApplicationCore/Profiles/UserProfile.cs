using AutoMapper;
using NotesApp.ApplicationCore.Commands.User;
using NotesApp.ApplicationCore.Dtos.User;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<RegisterUserDto, CreateUserCommand>();
    }
}