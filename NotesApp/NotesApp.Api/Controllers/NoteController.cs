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
    public async Task<IActionResult> CreateNoteDetail(CreateNoteRequest createNoteRequest, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateNoteDetailCommand>(createNoteRequest);
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToOk();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNoteDetails(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllNoteDetailsQuery(), cancellationToken);
        return result.ToOk();
    }
    
    [HttpGet(ApiRoutes.Notes.Trash)]
    public async Task<IActionResult> GetAllNoteDetailsTrash(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAllNoteDetailsTrashQuery(), cancellationToken);
        return result.ToOk();
    }
    
    [HttpGet(ApiRoutes.Notes.NoteDetailId)]
    public async Task<IActionResult> GetNoteDetailById(string noteDetailId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetNoteDetailByIdQuery(noteDetailId), cancellationToken);
        return result.ToOk();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateNoteDetail(UpdateNoteDetailRequest updateNoteDetailRequest, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateNoteDetailCommand>(updateNoteDetailRequest);
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToOk();
    }

    [HttpPut(ApiRoutes.Notes.Recover)]
    public async Task<IActionResult> RecoverNoteDetail(string noteDetailId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RecoverNoteDetailCommand(noteDetailId), cancellationToken);
        return result.ToOk();
    }
    
    [HttpDelete(ApiRoutes.Notes.SoftDeleteByNoteDetailId)]
    public async Task<IActionResult> SoftDeleteNoteDetail(string noteDetailId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SoftDeleteNoteDetailCommand(noteDetailId), cancellationToken);
        return result.ToOk();
    }
    
    [HttpDelete(ApiRoutes.Notes.NoteDetailId)]
    public async Task<IActionResult> DeleteNoteDetail(string noteDetailId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteNoteDetailCommand(noteDetailId), cancellationToken);
        return result.ToOk();
    }
}