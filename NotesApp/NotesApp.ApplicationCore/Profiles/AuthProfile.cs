using AutoMapper;
using NotesApp.ApplicationCore.Authentication.Commands.Register;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Profiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterCommand,User>();
    }
}