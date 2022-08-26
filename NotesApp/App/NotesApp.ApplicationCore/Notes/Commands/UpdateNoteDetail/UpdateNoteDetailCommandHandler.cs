using AutoMapper;
using FluentValidation;
using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.ApplicationCore.Services.NoteService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;

public class UpdateNoteDetailCommandHandler :  IRequestHandler<UpdateNoteDetailCommand, Result<NoteDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;
    private readonly IValidator<UpdateNoteDetailCommand> _validator;
    private readonly IAuthService _authService;
    private readonly INoteService _noteService;

    
    public UpdateNoteDetailCommandHandler(IHashids hashids, IUnitOfWork unitOfWork, IMapper mapper, 
        IValidator<UpdateNoteDetailCommand> validator, IAuthService authService, INoteService noteService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
        _authService = authService;
        _noteService = noteService;
        _hashids = hashids;
    }

    public async Task<Result<NoteDetailResponse>> Handle(UpdateNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var userId = _authService.GetUserIdByHttpContext();

        var exceptionResponse = ValidateRequest(request, userId);

        if (exceptionResponse.HasValue)
            return exceptionResponse.Value;

        var unHashId = _hashids.Decode(request.NoteDetailId)[0];

        var noteDetail = await _unitOfWork.Notes.GetNoteDetailByNoteDetailIdAndUserId
            (unHashId, userId.Value);

        if (noteDetail == null)
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);

        noteDetail = await UpdateNoteDetail(noteDetail,request);

        var noteDetailResponse = _mapper.Map<NoteDetailResponse>(noteDetail);
        noteDetailResponse.NoteDetailId = request.NoteDetailId;
        
        return noteDetailResponse;
    }

    private async Task<NoteDetail> UpdateNoteDetail(NoteDetail noteDetail, UpdateNoteDetailCommand request)
    {
        noteDetail.Title = request.Title;
        noteDetail.Description = request.Description;
        noteDetail.LastEdited = DateTime.Now;
        
        _unitOfWork.Notes.UpdateNoteDetail(noteDetail);
        await _unitOfWork.Save();
        
        return noteDetail;
    }
    private Result<NoteDetailResponse>? ValidateRequest(UpdateNoteDetailCommand request, int? userId)
    {
        var validation = _validator.Validate(request);
        
        if (!validation.IsValid)
            return new Result<NoteDetailResponse>(new ValidationException(validation.Errors));

        return _noteService.ValidateUserIdAndNoteDetailId<NoteDetailResponse>(userId, request.NoteDetailId);
    }

}