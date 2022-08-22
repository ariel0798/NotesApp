using AutoMapper;
using NotesApp.ApplicationCore.Authentication.Commands.Register;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Mapping;

public class AuthMappingConfig : Profile
{
    public AuthMappingConfig()
    {
        CreateMap<RegisterCommand,User>();

    }
}