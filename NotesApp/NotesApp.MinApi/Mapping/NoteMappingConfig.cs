using AutoMapper;
using NotesApp.ApplicationCore.Contracts.Note.Requests;
using NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;
using NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;

namespace NotesApp.MinApi.Mapping;

public class NoteMappingConfig: Profile
{
    public NoteMappingConfig()
    {
        CreateMap<CreateNoteRequest, CreateNoteDetailCommand>();
        CreateMap<UpdateNoteDetailRequest, UpdateNoteDetailCommand>();
    }
}