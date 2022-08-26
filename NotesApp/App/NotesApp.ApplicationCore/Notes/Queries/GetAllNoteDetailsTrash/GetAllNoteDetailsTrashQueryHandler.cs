using AutoMapper;
using HashidsNet;
using LanguageExt.Common;
using MediatR;
using NotesApp.ApplicationCore.Contracts.Notes.Responses;
using NotesApp.ApplicationCore.Services.AuthService;
using NotesApp.Domain.Errors.Exceptions.Factory;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Notes.Queries.GetAllNoteDetailsTrash;

public class GetAllNoteDetailsTrashQueryHandler :  IRequestHandler<GetAllNoteDetailsTrashQuery, Result<IEnumerable<NoteDetailResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;
    private readonly IAuthService _authService;

    
    public GetAllNoteDetailsTrashQueryHandler( IHashids hashids, IUnitOfWork unitOfWork, 
        IMapper mapper, IAuthService authService) 
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
        _hashids = hashids;
    }
    public async Task<Result<IEnumerable<NoteDetailResponse>>> Handle(GetAllNoteDetailsTrashQuery request, CancellationToken cancellationToken)
    {
        var userId = _authService.GetUserIdByHttpContext();

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