using AutoMapper;
using NotesApp.ApplicationCore.Contracts.Notes.Requests;
using NotesApp.ApplicationCore.Contracts.User.Requests;
using NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;
using NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;

namespace NotesApp.Api.Mapping;

public class NotesMappingConfig : Profile
{
    public NotesMappingConfig()
    {
        CreateMap<CreateNoteDetailRequest, CreateNoteDetailCommand>();
        CreateMap<UpdateNoteDetailRequest, UpdateNoteDetailCommand>();
    }
}