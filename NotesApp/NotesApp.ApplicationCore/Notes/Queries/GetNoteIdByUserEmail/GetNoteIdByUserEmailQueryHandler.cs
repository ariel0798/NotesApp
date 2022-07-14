using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.Queries.GetNoteIdByUserEmail;

public class GetNoteIdByUserEmailQueryHandler : IRequestHandler<GetNoteIdByUserEmailQuery,string?>
{
    private readonly IUserRepository _userRepository;
    private readonly INoteRepository _noteRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetNoteIdByUserEmailQueryHandler(IUserRepository userRepository, INoteRepository noteRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _noteRepository = noteRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string?> Handle(GetNoteIdByUserEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await GetUserByEmail(GetUserEmail());
        
        if (user == null)
            return null;

        var isExistingNote = await IsExistingNote(user.NoteId);
        
        return isExistingNote ?  user.NoteId : null;
    }
    private async Task<bool> IsExistingNote(string noteId)
    {
        var note = await _noteRepository.GetById(noteId);
        
        return note != null;
    }
    private async Task<User?> GetUserByEmail(string email)
    {
        var users = await _userRepository.GetAll();

        foreach (var user in users)
        {
            if (user.Email.Equals(email))
                return user;
        }
        return null;
    }
    private string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name); 
}