using AutoMapper;
using FluentValidation;
using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;

public class CreateNoteDetailCommandHandler :  IRequestHandler<CreateNoteDetailCommand, Result<NoteDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateNoteDetailCommand> _validator;
    private readonly IHashids _hashids;
    private readonly IAuthService _authService;


    public CreateNoteDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, 
        IValidator<CreateNoteDetailCommand> validator, IHashids hashids, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
        _hashids = hashids;
        _authService = authService;
    }

    public async Task<Result<NoteDetailResponse>> Handle(CreateNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var userId = _authService.GetUserIdByHttpContext();
        
        var exceptionResponse = ValidateRequest(request, userId);

        if (exceptionResponse.HasValue)
            return exceptionResponse.Value;
        
        var note = await _unitOfWork.Notes.GetNoteByUserId(userId.Value);
        
        if (note == null)
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);

        var noteDetail = _mapper.Map<NoteDetail>(request);
        
        noteDetail.NoteId = note.NoteId;

        await _unitOfWork.Notes.AddNoteDetail(noteDetail);
        await _unitOfWork.Save();
        
        var noteDetailDto = _mapper.Map<NoteDetailResponse>(noteDetail);
        noteDetailDto.NoteDetailId = _hashids.Encode(noteDetail.NoteDetailId);
        
        return noteDetailDto;
    }

    private Result<NoteDetailResponse>? ValidateRequest(CreateNoteDetailCommand request, int? userId)
    {
        var validationResult =  _validator.Validate(request);

        if (!validationResult.IsValid)
            return new Result<NoteDetailResponse>(new ValidationException(validationResult.Errors));

        return _authService.ValidateUserId<NoteDetailResponse>(userId);
    }
}