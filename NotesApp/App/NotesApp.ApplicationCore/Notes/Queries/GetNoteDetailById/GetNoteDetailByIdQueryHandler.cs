using AutoMapper;
using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.Queries.GetNoteDetailById;

public class GetNoteDetailByIdQueryHandler :  IRequestHandler<GetNoteDetailByIdQuery, Result<NoteDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;
    private readonly IAuthService _authService;

    
    public GetNoteDetailByIdQueryHandler(IAuthService authService, IHashids hashids,
        IUnitOfWork unitOfWork, IMapper mapper) 
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
        _hashids = hashids;
    }
    public  async Task<Result<NoteDetailResponse>> Handle(GetNoteDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _authService.GetUserIdByHttpContext();

        var exceptionResponse = ValidateRequest(request, userId);

        if (exceptionResponse.HasValue)
            return exceptionResponse.Value;

        var unHashId = _hashids.Decode(request.NoteDetailId)[0];
            
        var noteDetail = await _unitOfWork.Notes.GetNoteDetailByNoteDetailIdAndUserId(unHashId, userId.Value);
        
        if (noteDetail == null)
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);
        
        var noteDetailDto = _mapper.Map<NoteDetailResponse>(noteDetail);
        noteDetailDto.NoteDetailId = request.NoteDetailId;
        
        return noteDetailDto;
    }

    private Result<NoteDetailResponse>? ValidateRequest(GetNoteDetailByIdQuery request, int? userId)
    {
        if(!_authService.IsIdRightLenght(request.NoteDetailId))
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);

        var hashId = _hashids.Decode(request.NoteDetailId);

        if (!HasHashIdValue(hashId))
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);

        if (userId == null)
            return new Result<NoteDetailResponse>(ExceptionFactory.InvalidCredentialException);

        return null;
    }
    
    private bool HasHashIdValue(int[]? hashId) => hashId.Length > 0;

}