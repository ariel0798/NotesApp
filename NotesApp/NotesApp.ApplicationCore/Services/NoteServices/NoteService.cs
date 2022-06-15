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
    
    
    public async Task<NoteDetail> CreateNote(CreateNoteDto noteDto)
    {
        var userQuery = new GetUserByEmailQuery() { Email = _authService.GetUserEmail() };

        var user = await _mediator.Send(userQuery);

        
        if (user == null)
            return null;

        var isExistingNote = await IsExistingNote(user.NoteId);
        if (!isExistingNote)
            return null;

        var command = _mapper.Map <CreateNoteDetailCommand>(noteDto);
        command.NoteId = user.NoteId;

        return await _mediator.Send(command);
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