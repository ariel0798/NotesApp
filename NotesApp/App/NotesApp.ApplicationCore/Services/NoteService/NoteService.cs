using HashidsNet;
using LanguageExt.Common;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Errors.Exceptions.Factory;

namespace NotesApp.ApplicationCore.Services.NoteService;

public class NoteService : INoteService
{
    private readonly IAuthService _authService;
    private readonly IHashids _hashids;

    public NoteService(IAuthService authService, IHashids hashids)
    {
        _authService = authService;
        _hashids = hashids;
    }

    public Result<T>? ValidateUserIdAndNoteDetailId<T>(int? userId, string noteDetailId)
    {
        if(!_authService.IsIdRightLenght(noteDetailId))
            return new Result<T>(ExceptionFactory.NoteNotFoundException);

        var hashId = _hashids.Decode(noteDetailId);

        if (!HasHashIdValue(hashId))
            return new Result<T>(ExceptionFactory.NoteNotFoundException);

        return _authService.ValidateUserId<T>(userId);
    }
    
    private bool HasHashIdValue(int[]? hashId) => hashId.Length > 0;

}