using System.Security.Claims;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NotesApp.ApplicationCore.Common.Models;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes;

public class NoteBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHashids _hashids;
    private readonly HashIdSettings _hashIdSettings;


    public NoteBase( IHttpContextAccessor httpContextAccessor, IHashids hashids, IOptions<HashIdSettings> hashSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _hashids = hashids;
        _hashIdSettings = hashSettings.Value;
    }
    
    protected int? GetUserIdByHttpContext()
    {
        var hashUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!IsIdRightLenght(hashUserId))
            return null;
        
        var decodedId = _hashids.Decode(hashUserId);
        return decodedId.Any() ? decodedId[0] : null;
    }

    protected bool IsIdRightLenght(string id)
    {
        return id.Length >= _hashIdSettings.MinimumHashIdLenght;
    }
}