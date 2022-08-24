using AutoMapper;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;
using NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Mapping;

public class NoteMappingConfig : Profile
{
    public NoteMappingConfig()
    {
        CreateMap<NoteDetail, NoteDetailResponse>();
        CreateMap<CreateNoteDetailCommand, NoteDetail>();
    }
}