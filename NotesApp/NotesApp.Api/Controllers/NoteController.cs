using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Extensions;
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
        return result.ToOk();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNoteDetails()
    {
        var result = await _noteService.GetAllNoteDetails();
        return result.ToOk();
    }
    
    [HttpGet(ApiRoutes.Notes.Trash)]
    public async Task<IActionResult> GetAllNoteDetailsTrash()
    {
        var result = await _noteService.GetAllNoteDetailsTrash();
        return result.ToOk();
    }
    
    [HttpGet(ApiRoutes.Notes.NoteDetailId)]
    public async Task<IActionResult> GetNoteDetailById(string noteDetailId)
    {
        var result = await _noteService.GetNoteDetailById(noteDetailId);
        return result.ToOk();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNoteDetail(UpdateNoteDetailRequest updateNoteDetailRequest)
    {
        var result = await _noteService.UpdateNoteDetail(updateNoteDetailRequest);
        return result.ToOk();
    }

    [HttpPut(ApiRoutes.Notes.Recover)]
    public async Task<IActionResult> RecoverNoteDetail(string noteDetailId)
    {
        var result = await _noteService.RecoverNoteDetail(noteDetailId);
        return result.ToOk();
    }
    
    [HttpDelete(ApiRoutes.Notes.SoftDeleteByNoteDetailId)]
    public async Task<IActionResult> SoftDeleteNoteDetail(string noteDetailId)
    {
        var result = await _noteService.SoftDeleteNoteDetail(noteDetailId);
        return result.ToOk();
    }
    
    [HttpDelete(ApiRoutes.Notes.NoteDetailId)]
    public async Task<IActionResult> DeleteNoteDetail(string noteDetailId)
    {
        var result = await _noteService.DeleteNoteDetail(noteDetailId);
        return result.ToOk();
    }
}