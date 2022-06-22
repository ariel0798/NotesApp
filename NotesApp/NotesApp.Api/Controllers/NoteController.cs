using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.ApplicationCore.Dtos.Note;
using NotesApp.ApplicationCore.Services.NoteServices;

namespace NotesApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NoteController : Controller
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    [HttpPost]
    
    public async Task<IActionResult> CreateNoteDetail(CreateNoteDto noteDto)
    {
        var result = await _noteService.CreateNoteDetail(noteDto);
        return Ok(result);
    }

    [HttpGet("{noteDetailId}")]
    public async Task<IActionResult> GetNoteDetailById(string noteDetailId)
    {
        var result = await _noteService.GetNoteDetailById(noteDetailId);
        return Ok(result);
    }
}