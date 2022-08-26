using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.Commands.DeleteNoteDetail;

public class DeleteNoteDetailCommandHandler : IRequestHandler<DeleteNoteDetailCommand,Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashids _hashids;
    private readonly IAuthService _authService;
    
    public DeleteNoteDetailCommandHandler(IHashids hashids, IUnitOfWork unitOfWork, IAuthService authService) 

    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _hashids = hashids;
    }
    public async Task<Result<bool>> Handle(DeleteNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var userId = _authService.GetUserIdByHttpContext();

        var exceptionResponse = ValidateRequest(request, userId);

        if (exceptionResponse.HasValue)
            return exceptionResponse.Value;

        var unHashId = _hashids.Decode(request.NoteDetailId)[0];
        
        var noteDetail = await _unitOfWork.Notes.GetNoteDetailByNoteDetailIdAndUserId(unHashId, userId.Value);
        
        if (noteDetail == null)
            return new Result<bool>(ExceptionFactory.NoteNotFoundException);

        await _unitOfWork.Notes.DeleteNoteDetail(unHashId, userId.Value);
        await _unitOfWork.Save();
        
        return true;
    }
    
    private Result<bool>? ValidateRequest(DeleteNoteDetailCommand request, int? userId)
    {
        if (userId == null)
            return new Result<bool>(ExceptionFactory.InvalidCredentialException);
        
        if(!_authService.IsIdRightLenght(request.NoteDetailId))
            return new Result<bool>(ExceptionFactory.NoteNotFoundException);

        var hashId = _hashids.Decode(request.NoteDetailId);

        if (!HasHashIdValue(hashId))
            return new Result<bool>(ExceptionFactory.NoteNotFoundException);
        
        return null;
    }
    
    private bool HasHashIdValue(int[]? hashId) => hashId.Length > 0;
}