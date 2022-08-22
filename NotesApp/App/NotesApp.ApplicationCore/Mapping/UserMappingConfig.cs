using AutoMapper;
using NotesApp.ApplicationCore.Contracts.User.Responses;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Mapping;

public class UserMappingConfig : Profile
{
    public UserMappingConfig()
    {
        CreateMap<User, UserResponse>();
    }
}