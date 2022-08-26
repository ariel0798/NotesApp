using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.ApplicationCore.Services.NoteService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Commands.SoftDeleteNoteDetail;

public class SoftDeleteNoteDetailCommandHandler : IRequestHandler<SoftDeleteNoteDetailCommand,Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashids _hashids;
    private readonly IAuthService _authService;
    private readonly INoteService _noteService;
    
    public SoftDeleteNoteDetailCommandHandler(IHashids hashids, IUnitOfWork unitOfWork, IAuthService authService, INoteService noteService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _noteService = noteService;
        _hashids = hashids;
    }
    
    public async Task<Result<bool>> Handle(SoftDeleteNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var userId = _authService.GetUserIdByHttpContext();

        var exceptionResponse = _noteService.ValidateUserIdAndNoteDetailId<bool>(userId, request.NoteDetailId);

        if (exceptionResponse.HasValue)
            return exceptionResponse.Value;

        var unHashId = _hashids.Decode(request.NoteDetailId)[0];
        
        var noteDetail = await _unitOfWork.Notes.GetNoteDetailByNoteDetailIdAndUserId(unHashId, userId.Value);
        
        if (noteDetail == null)
            return new Result<bool>(ExceptionFactory.NoteNotFoundException);

        await SoftDeleteNoteDetail(noteDetail);

        return true;
    }

    private async Task SoftDeleteNoteDetail(NoteDetail noteDetail)
    {
        noteDetail.IsDeleted = true;
        noteDetail.DeletedDate = DateTime.Now;
        noteDetail.LastEdited = DateTime.Now;
        
        _unitOfWork.Notes.UpdateNoteDetail(noteDetail);

        await _unitOfWork.Save();
    }
}