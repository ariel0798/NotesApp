using AutoMapper;
using NotesApp.ApplicationCore.Dtos.Note;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<CreateNoteDto, CreateNoteDetailCommand>();
        CreateMap<CreateNoteDetailCommand, NoteDetail>();
        CreateMap<NoteDetail, GetNoteDetailDto>(MemberList.Destination);
        CreateMap<UpdateNoteDetailDto, UpdateNoteDetailCommand>();
    }
}