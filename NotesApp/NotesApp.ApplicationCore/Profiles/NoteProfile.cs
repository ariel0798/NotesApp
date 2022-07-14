using AutoMapper;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<NoteDetail, GetNoteDetailResponse>();
        CreateMap<CreateNoteDetailCommand, NoteDetail>();
    }
}