using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Api.Extensions;
using NotesApp.ApplicationCore.Contracts.Note.Requests;
using NotesApp.ApplicationCore.Notes.Commands.CreateNoteDetail;
using NotesApp.ApplicationCore.Notes.Commands.DeleteNoteDetail;
using NotesApp.ApplicationCore.Notes.Commands.RecoverNoteDetail;
using NotesApp.ApplicationCore.Notes.Commands.SoftDeleteNoteDetail;
using NotesApp.ApplicationCore.Notes.Commands.UpdateNoteDetail;
using NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetails;
using NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetailsTrash;
using NotesApp.ApplicationCore.Notes.Queries.GetNoteDetailById;

namespace NotesApp.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class NoteController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public NoteController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNoteDetail(CreateNoteRequest createNoteRequest)
    {
        var command = _mapper.Map<CreateNoteDetailCommand>(createNoteRequest);
        var result = await _mediator.Send(command);
        return result.ToOk();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNoteDetails()
    {
        var result = await _mediator.Send(new GetAllNoteDetailsQuery());
        return result.ToOk();
    }
    
    [HttpGet(ApiRoutes.Notes.Trash)]
    public async Task<IActionResult> GetAllNoteDetailsTrash()
    {
        var result = await _mediator.Send(new GetAllNoteDetailsTrashQuery());
        return result.ToOk();
    }
    
    [HttpGet(ApiRoutes.Notes.NoteDetailId)]
    public async Task<IActionResult> GetNoteDetailById(string noteDetailId)
    {
        var result = await _mediator.Send(new GetNoteDetailByIdQuery(noteDetailId));
        return result.ToOk();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNoteDetail(UpdateNoteDetailRequest updateNoteDetailRequest)
    {
        var command = _mapper.Map<UpdateNoteDetailCommand>(updateNoteDetailRequest);
        var result = await _mediator.Send(command);
        return result.ToOk();
    }

    [HttpPut(ApiRoutes.Notes.Recover)]
    public async Task<IActionResult> RecoverNoteDetail(string noteDetailId)
    {
        var result = await _mediator.Send(new RecoverNoteDetailCommand(noteDetailId));
        return result.ToOk();
    }
    
    [HttpDelete(ApiRoutes.Notes.SoftDeleteByNoteDetailId)]
    public async Task<IActionResult> SoftDeleteNoteDetail(string noteDetailId)
    {
        var result = await _mediator.Send(new SoftDeleteNoteDetailCommand(noteDetailId));
        return result.ToOk();
    }
    
    [HttpDelete(ApiRoutes.Notes.NoteDetailId)]
    public async Task<IActionResult> DeleteNoteDetail(string noteDetailId)
    {
        var result = await _mediator.Send(new DeleteNoteDetailCommand(noteDetailId));
        return result.ToOk();
    }
}