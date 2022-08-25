using AutoMapper;
using HashidsNet;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NotesApp.ApplicationCore.Common.Models;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetailsTrash;

public class GetAllNoteDetailsTrashQueryHandler : NoteBase,  IRequestHandler<GetAllNoteDetailsTrashQuery, Result<IEnumerable<NoteDetailResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;
    
    public GetAllNoteDetailsTrashQueryHandler(IHttpContextAccessor httpContextAccessor, IHashids hashids,
        IOptions<HashIdSettings> hashSettings, IUnitOfWork unitOfWork, IMapper mapper) 
        : base(httpContextAccessor, hashids, hashSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hashids = hashids;
    }
    public async Task<Result<IEnumerable<NoteDetailResponse>>> Handle(GetAllNoteDetailsTrashQuery request, CancellationToken cancellationToken)
    {
        var userId = GetUserIdByHttpContext();

        if (userId == null)
            return new Result<IEnumerable<NoteDetailResponse>>(ExceptionFactory.InvalidCredentialException);

        var noteDetails = await _unitOfWork.Notes.GetAllNoteDetailsByUserId(userId.Value);
        var noteDetailNonDeleted = noteDetails.Where(x => x.IsDeleted == true);

        var noteDetailResponses = new List<NoteDetailResponse>();
        
        foreach (var noteDetail in noteDetailNonDeleted)
        {
            var noteDetailDto = _mapper.Map<NoteDetailResponse>(noteDetail);
            noteDetailDto.NoteDetailId = _hashids.Encode(noteDetail.NoteDetailId);
            noteDetailResponses.Add(noteDetailDto);
        }
        
        return noteDetailResponses;
    }


}