using AutoMapper;
using NotesApp.ApplicationCore.Contracts.Notes.Requests;
using NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;

namespace NotesApp.Api.Mapping;

public class NotesMappingConfig : Profile
{
    public NotesMappingConfig()
    {
        CreateMap<CreateNoteDetailRequest, CreateNoteDetailCommand>();
    }
}