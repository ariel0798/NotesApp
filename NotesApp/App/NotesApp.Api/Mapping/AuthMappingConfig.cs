using AutoMapper;
using NotesApp.ApplicationCore.Authentication.Commands.Register;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;

namespace NotesApp.Api.Mapping;

public class AuthMappingConfig : Profile
{
    public AuthMappingConfig()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
    }
}