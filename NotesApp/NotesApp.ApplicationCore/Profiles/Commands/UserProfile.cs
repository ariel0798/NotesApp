using AutoMapper;
using NotesApp.ApplicationCore.Commands.User;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Profiles.Commands;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommand, User>();
    }
}