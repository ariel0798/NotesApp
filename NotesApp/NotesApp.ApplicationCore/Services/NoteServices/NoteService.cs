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
    private readonly IValidator<UpdateNoteDetailRequest> _validatorUpdateNote;
        

    public NoteService(IMediator mediator, IMapper mapper, IAuthService authService, IValidator<CreateNoteRequest> validatorCreateNote, IValidator<UpdateNoteDetailRequest> validatorUpdateNote)
    {
        _mediator = mediator;
        _mapper = mapper;
        _authService = authService;
        _validatorCreateNote = validatorCreateNote;
        _validatorUpdateNote = validatorUpdateNote;
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

    public async Task<Result<List<GetNoteDetailResponse>>> GetAllNoteDetails()
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return new Result<List<GetNoteDetailResponse>>().CreateException<List<GetNoteDetailResponse>,NoteNotFoundException>();

        var query = new GetNoteByIdQuery() { NoteId = noteId };

        var note = await _mediator.Send(query);

        var noteDetailNonDeleted = note.NoteDetails.Where(n => n.IsDeleted == false);
        var noteDetailsDto = _mapper.Map<List<GetNoteDetailResponse>>(noteDetailNonDeleted);
        return noteDetailsDto;
    }
    
    public async Task<Result<List<GetNoteDetailResponse>>> GetAllNoteDetailsTrash()
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return new Result<List<GetNoteDetailResponse>>().CreateException<List<GetNoteDetailResponse>,NoteNotFoundException>();


        var query = new GetNoteByIdQuery() { NoteId = noteId };

        var note = await _mediator.Send(query);

        var noteDetailNonDeleted = note.NoteDetails.Where(n => n.IsDeleted == true);
        var noteDetailsDto = _mapper.Map<List<GetNoteDetailResponse>>(noteDetailNonDeleted);
        return noteDetailsDto;
    }
    public async Task<Result<GetNoteDetailResponse>> GetNoteDetailById(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse,NoteNotFoundException>();


        var query = new GetNoteDetailByIdQuery()
        {
            NoteId = noteId,
            NoteDetailId = noteDetailId
        };

        var noteDetail = await _mediator.Send(query);
        
        if (noteDetail == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse,NoteNotFoundException>();
        
        var noteDetailDto = _mapper.Map<GetNoteDetailResponse>(noteDetail);
        
        return noteDetailDto;
    }

    public async Task<Result<GetNoteDetailResponse>> UpdateNoteDetail(UpdateNoteDetailRequest noteDetailDto)
    {
        var validationResult = await _validatorUpdateNote.ValidateAsync(noteDetailDto);
        if (!validationResult.IsValid)
            return new Result<GetNoteDetailResponse>().CreateValidationException(validationResult.Errors);
        
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse,NoteNotFoundException>();

        var command = _mapper.Map<UpdateNoteDetailCommand>(noteDetailDto);

        command.NoteId = noteId;
        
        var noteDetail = await _mediator.Send(command);
        
        if (noteDetail == null)
            return new Result<GetNoteDetailResponse>().CreateException<GetNoteDetailResponse,NoteNotFoundException>();

        var noteDetailGetDto = _mapper.Map<GetNoteDetailResponse>(noteDetail);
        
        return noteDetailGetDto;
    }

    public async Task<Result<bool>> RecoverNoteDetail(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();

        var command = new RecoverNoteDetailCommand()
        {
            NoteDetailId = noteDetailId,
            NoteId = noteId
        };

        var noteDetail = await _mediator.Send(command);
        
        if (noteDetail == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();

        return noteDetail.IsDeleted;
    }
    
    public async Task<Result<bool>> SoftDeleteNoteDetail(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();

        var command = new SoftDeleteNoteDetailCommand()
        {
            NoteDetailId = noteDetailId,
            NoteId = noteId
        };

        var noteDetail = await _mediator.Send(command);
        
        if (noteDetail == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();
        
        return noteDetail.IsDeleted;
    }
    
    public async Task<Result<bool>> DeleteNoteDetail(string noteDetailId)
    {
        var noteId = await GetNoteId();
        
        if (noteId == null)
            return new Result<bool>().CreateException<bool,NoteNotFoundException>();

        var command = new DeleteNoteDetailCommand()
        {
            NoteDetailId = noteDetailId,
            NoteId = noteId
        };
        
        var noteDetail =  await _mediator.Send(command);

        return noteDetail != null ? true : new Result<bool>().CreateException<bool,NoteNotFoundException>(); ;
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