using AutoMapper;
using MediatR;
using NotesApp.ApplicationCore.Dtos.Note;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.ApplicationCore.Notes.Queries;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.ApplicationCore.Users.Queries;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public class NoteService : INoteService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public NoteService(IMediator mediator, IMapper mapper, IAuthService authService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _authService = authService;
    }
    
    
    public async Task<NoteDetail> CreateNoteDetail(CreateNoteDto noteDto)
    {
        var noteId = await GetNoteId();

        if (noteId == null)
            return null;
        
        var command = _mapper.Map <CreateNoteDetailCommand>(noteDto);
        command.NoteId = noteId;

        return await _mediator.Send(command);
    }

    public async Task<List<NoteDetail>> GetAllNoteDetails()
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return null;

        var query = new GetNoteByIdQuery() { NoteId = noteId };

        var note = await _mediator.Send(query);

        return note.NoteDetails;
    }
    public async Task<NoteDetail?> GetNoteDetailById(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return null;

        var query = new GetNoteDetailByIdQuery()
        {
            NoteId = noteId,
            NoteDetailId = noteDetailId
        };

        return await _mediator.Send(query);
    }


    private async Task<string?> GetNoteId()
    {
        var userQuery = new GetUserByEmailQuery() { Email = _authService.GetUserEmail() };

        var user = await _mediator.Send(userQuery);

        
        if (user == null)
            return null;

        var isExistingNote = await IsExistingNote(user.NoteId);
        
        return  user.NoteId;
    }
    
    private async Task<bool> IsExistingNote(string noteId)
    {
        var query = new NoteIdExistQuery()
        {
            Id = noteId
        };
        
        return await _mediator.Send(query);
    }
}