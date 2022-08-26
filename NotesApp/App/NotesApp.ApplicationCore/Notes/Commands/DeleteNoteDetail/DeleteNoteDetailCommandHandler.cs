using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.ApplicationCore.Services.NoteService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.Commands.DeleteNoteDetail;

public class DeleteNoteDetailCommandHandler : IRequestHandler<DeleteNoteDetailCommand,Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashids _hashids;
    private readonly IAuthService _authService;
    private readonly INoteService _noteService;
    
    public DeleteNoteDetailCommandHandler(IHashids hashids, IUnitOfWork unitOfWork, IAuthService authService, INoteService noteService) 

    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _noteService = noteService;
        _hashids = hashids;
    }
    public async Task<Result<bool>> Handle(DeleteNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var userId = _authService.GetUserIdByHttpContext();

        var exceptionResponse = _noteService.ValidateUserIdAndNoteDetailId<bool>(userId,request.NoteDetailId);

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
}