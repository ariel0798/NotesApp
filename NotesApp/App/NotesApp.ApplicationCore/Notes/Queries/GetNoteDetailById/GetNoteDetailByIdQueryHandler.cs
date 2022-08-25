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

namespace NotesApp.ApplicationCore.Notes.Queries.GetNoteDetailById;

public class GetNoteDetailByIdQueryHandler : NoteBase, IRequestHandler<GetNoteDetailByIdQuery, Result<NoteDetailResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;
    
    public GetNoteDetailByIdQueryHandler(IHttpContextAccessor httpContextAccessor, IHashids hashids, 
        IOptions<HashIdSettings> hashIdOptionsSettings,
        IUnitOfWork unitOfWork, IMapper mapper) 
        : base(httpContextAccessor, hashids, hashIdOptionsSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hashids = hashids;
    }
    public  async Task<Result<NoteDetailResponse>> Handle(GetNoteDetailByIdQuery request, CancellationToken cancellationToken)
    {
        if(!IsIdRightLenght(request.NoteDetailId))
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);

        var hashId = _hashids.Decode(request.NoteDetailId);

        if (hashId.Length == 0)
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);
        
        var unHashId = hashId[0];
        var userId = GetUserIdByHttpContext();

        if (userId == null)
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);
        
        var noteDetail = await _unitOfWork.Notes.GetNoteDetailByNoteDetailIdAndUserId(unHashId, userId.Value);
        
        if (noteDetail == null)
            return new Result<NoteDetailResponse>(ExceptionFactory.NoteNotFoundException);
        
        var noteDetailDto = _mapper.Map<NoteDetailResponse>(noteDetail);
        noteDetailDto.NoteDetailId = request.NoteDetailId;
        
        return noteDetailDto;
    }
}