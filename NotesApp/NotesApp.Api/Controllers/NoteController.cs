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
        var result = await _noteService.CreateNote(noteDto);
        return Ok(result);
    }
   
}