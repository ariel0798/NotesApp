using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.ApplicationCore.Contracts.Note.Requests;
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
    
    public async Task<IActionResult> CreateNoteDetail(CreateNoteRequest createNoteRequest)
    {
        var result = await _noteService.CreateNoteDetail(createNoteRequest);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNoteDetails()
    {
        var result = await _noteService.GetAllNoteDetails();
        return Ok(result);
    }
    
    [HttpGet("trash")]
    public async Task<IActionResult> GetAllNoteDetailsTrash()
    {
        var result = await _noteService.GetAllNoteDetailsTrash();
        return Ok(result);
    }
    
    [HttpGet("{noteDetailId}")]
    public async Task<IActionResult> GetNoteDetailById(string noteDetailId)
    {
        var result = await _noteService.GetNoteDetailById(noteDetailId);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNoteDetail(UpdateNoteDetailRequest updateNoteDetailRequest)
    {
        var result = await _noteService.UpdateNoteDetail(updateNoteDetailRequest);
        return Ok(result);
    }

    [HttpPut("recover")]
    public async Task<IActionResult> RecoverNoteDetail(string noteDetailId)
    {
        var result = await _noteService.RecoverNoteDetail(noteDetailId);
        return Ok(result);
    }
    
    [HttpDelete("{noteDetailId}/soft-delete")]
    public async Task<IActionResult> SoftDeleteNoteDetail(string noteDetailId)
    {
        var result = await _noteService.SoftDeleteNoteDetail(noteDetailId);
        return Ok(result);
    }
    
    [HttpDelete("{noteDetailId}")]
    public async Task<IActionResult> DeleteNoteDetail(string noteDetailId)
    {
        var result = await _noteService.DeleteNoteDetail(noteDetailId);
        return Ok(result);
    }
}