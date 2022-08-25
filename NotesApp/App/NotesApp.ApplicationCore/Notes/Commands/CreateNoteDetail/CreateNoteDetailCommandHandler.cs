using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using HashidsNet;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using NotesApp.ApplicationCore.Common.Models;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;

public class CreateNoteDetailCommandHandler : NoteBase, IRequestHandler<CreateNoteDetailCommand, Result<NoteDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateNoteDetailCommand> _validator;
    private readonly IHashids _hashids;


    public CreateNoteDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, 
        IValidator<CreateNoteDetailCommand> validator, IHashids hashids, IHttpContextAccessor httpContextAccessor,
        IOptions<HashIdSettings> hashIdOptions) 
        : base(httpContextAccessor, hashids,hashIdOptions)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
        _hashids = hashids;
    }

    public async Task<Result<NoteDetailResponse>> Handle(CreateNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request,cancellationToken);

        if (!validationResult.IsValid)
            return new Result<NoteDetailResponse>(new ValidationException(validationResult.Errors));

        var userId = GetUserIdByHttpContext();

        if (userId == null)
            return new Result<NoteDetailResponse>(ExceptionFactory.InvalidCredentialException);
        
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

    private async Task<Note?> GetNoteId()
    {
        var userId = GetUserIdByHttpContext();

        if (userId.HasValue)
            return await _unitOfWork.Notes.GetNoteByUserId(userId.Value);

        return null;
    }
}