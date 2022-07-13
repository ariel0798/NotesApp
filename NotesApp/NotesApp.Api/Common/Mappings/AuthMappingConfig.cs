using AutoMapper;
using NotesApp.ApplicationCore.Authentication.Commands.Login;
using NotesApp.ApplicationCore.Authentication.Commands.Register;
using NotesApp.ApplicationCore.Contracts.Authentication.Requests;

namespace NotesApp.Api.Common.Mappings;

public class AuthMappingConfig: Profile
{
    public AuthMappingConfig()
    {
        CreateMap<RegisterUserRequest, RegisterCommand>();
        CreateMap<LoginRequest, LoginCommand>();
    }
}