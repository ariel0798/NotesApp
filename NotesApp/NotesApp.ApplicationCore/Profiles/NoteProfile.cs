using AutoMapper;
using NotesApp.ApplicationCore.Contracts.Note.Requests;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<CreateNoteRequest, CreateNoteDetailCommand>();
        CreateMap<CreateNoteDetailCommand, NoteDetail>();
        CreateMap<NoteDetail, GetNoteDetailResponse>(MemberList.Destination);
        CreateMap<UpdateNoteDetailRequest, UpdateNoteDetailCommand>();
    }
}