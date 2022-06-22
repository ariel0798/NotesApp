using AutoMapper;
using MediatR;
using NotesApp.ApplicationCore.Notes.Commands;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;

namespace NotesApp.ApplicationCore.Notes.CommandHandlers;

public class CreateNoteDetailCommandHandler : IRequestHandler<CreateNoteDetailCommand,NoteDetail>
{
    private readonly INoteRepository _noteRepository;
    private readonly IMapper _mapper;

    public CreateNoteDetailCommandHandler(INoteRepository noteRepository, IMapper mapper)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
    }
    public async Task<NoteDetail> Handle(CreateNoteDetailCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetById(request.NoteId);

        note.NoteDetails ??= new List<NoteDetail>();
        
        var noteDetail = _mapper.Map<NoteDetail>(request);
        note.NoteDetails.Add(noteDetail);

        await _noteRepository.Update(note);
        return noteDetail;
    }
}