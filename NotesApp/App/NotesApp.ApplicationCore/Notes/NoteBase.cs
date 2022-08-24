using System.Security.Claims;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Http;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes;

public class NoteBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHashids _hashids;


    public NoteBase( IHttpContextAccessor httpContextAccessor, IHashids hashids)
    {
        _httpContextAccessor = httpContextAccessor;
        _hashids = hashids;
    }
    
    protected int? GetUserIdByHttpContext()
    {
        var hashUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var decodedId = _hashids.Decode(hashUserId);

        return decodedId.Any() ? decodedId[0] : null;
    }
}