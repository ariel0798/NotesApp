using AutoMapper;
using FluentValidation;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Note.Requests;
using NotesApp.ApplicationCore.Contracts.Note.Responses;
using NotesApp.ApplicationCore.Extensions;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.ApplicationCore.Notes.Queries;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.ApplicationCore.Users.Queries;
using NotesApp.Domain.Errors.Exceptions;

namespace NotesApp.ApplicationCore.Services.NoteServices;

public class NoteService : INoteService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IValidator<CreateNoteRequest> _validatorCreateNote;
        

    public NoteService(IMediator mediator, IMapper mapper, IAuthService authService, IValidator<CreateNoteRequest> validatorCreateNote)
    {
        _mediator = mediator;
        _mapper = mapper;
        _authService = authService;
        _validatorCreateNote = validatorCreateNote;
    }
    
    
    public async Task<Result<GetNoteDetailResponse>> CreateNoteDetail(CreateNoteRequest createNoteRequest)
    {
        var validationResult = await _validatorCreateNote.ValidateAsync(createNoteRequest);
        if (!validationResult.IsValid)
            return new Result<GetNoteDetailResponse>().CreateValidationException(validationResult.Errors);
        
        var noteId = await GetNoteId();

        if (noteId == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse, NoteNotFoundException>();
        
        var command = _mapper.Map <CreateNoteDetailCommand>(createNoteRequest);
        command.NoteId = noteId;

        var noteDetail = await _mediator.Send(command);

        var noteDetailDto = _mapper.Map<GetNoteDetailResponse>(noteDetail);
        
        return noteDetailDto;
    }

    public async Task<List<GetNoteDetailResponse>> GetAllNoteDetails()
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return null;

        var query = new GetNoteByIdQuery() { NoteId = noteId };

        var note = await _mediator.Send(query);

        var noteDetailNonDeleted = note.NoteDetails.Where(n => n.IsDeleted == false);
        var noteDetailsDto = _mapper.Map<List<GetNoteDetailResponse>>(noteDetailNonDeleted);
        return noteDetailsDto;
    }
    
    public async Task<List<GetNoteDetailResponse>> GetAllNoteDetailsTrash()
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return null;

        var query = new GetNoteByIdQuery() { NoteId = noteId };

        var note = await _mediator.Send(query);

        var noteDetailNonDeleted = note.NoteDetails.Where(n => n.IsDeleted == true);
        var noteDetailsDto = _mapper.Map<List<GetNoteDetailResponse>>(noteDetailNonDeleted);
        return noteDetailsDto;
    }
    public async Task<GetNoteDetailResponse?> GetNoteDetailById(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return null;

        var query = new GetNoteDetailByIdQuery()
        {
            NoteId = noteId,
            NoteDetailId = noteDetailId
        };

        var noteDetail = await _mediator.Send(query);
        
        var noteDetailDto = _mapper.Map<GetNoteDetailResponse>(noteDetail);
        
        return noteDetailDto;
    }

    public async Task<GetNoteDetailResponse> UpdateNoteDetail(UpdateNoteDetailRequest noteDetailDto)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return null;

        var command = _mapper.Map<UpdateNoteDetailCommand>(noteDetailDto);

        command.NoteId = noteId;
        
        var noteDetail = await _mediator.Send(command);
        
        var noteDetailGetDto = _mapper.Map<GetNoteDetailResponse>(noteDetail);
        
        return noteDetailGetDto;
    }

    public async Task<bool> RecoverNoteDetail(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return false;

        var command = new RecoverNoteDetailCommand()
        {
            NoteDetailId = noteDetailId,
            NoteId = noteId
        };

        var noteDetail = await _mediator.Send(command);

        return noteDetail.IsDeleted;
    }
    
    public async Task<bool> SoftDeleteNoteDetail(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return false;

        var command = new SoftDeleteNoteDetailCommand()
        {
            NoteDetailId = noteDetailId,
            NoteId = noteId
        };

        var noteDetail = await _mediator.Send(command);

        return noteDetail.IsDeleted;
    }
    
    public async Task<bool> DeleteNoteDetail(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return false;

        var command = new DeleteNoteDetailCommand()
        {
            NoteDetailId = noteDetailId,
            NoteId = noteId
        };
        await _mediator.Send(command);

        return await _mediator.Send(command) == null ? true : false ;
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