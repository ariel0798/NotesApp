using Dapper;
using Microsoft.EntityFrameworkCore;
using NotesApp.Domain.Interfaces;
using NotesApp.Domain.Models;
using NotesApp.Infrastructure.Context;
using NotesApp.Infrastructure.DatabaseProvider;

namespace NotesApp.Infrastructure.Repositories;

public class NoteRepository :  GenericRepository<Note>, INoteRepository
{
    private readonly IDatabaseProvider _databaseProvider;
    private readonly DbSet<NoteDetail> _tableNoteDetails;

    public NoteRepository(NotesAppDbContext context, IDatabaseProvider databaseProvider) 
        : base(context)
    {
        _databaseProvider = databaseProvider;
        _tableNoteDetails = context.Set<NoteDetail>();
    }

    public async Task<NoteDetail> AddNoteDetail(NoteDetail noteDetail)
    {
        await _tableNoteDetails.AddAsync(noteDetail);
        return noteDetail;
    }

    public void UpdateNoteDetail(NoteDetail noteDetail)
    {
        _tableNoteDetails.Update(noteDetail);
    }
    

    public async Task<NoteDetail> GetNoteDetailByNoteDetailId(int noteDetailId)
    {
        return  await GetNoteDetailById(noteDetailId);
    }

    public async Task<IEnumerable<NoteDetail>> GetAllNoteDetailsByUserId(int userId)
    {
        await using var connection = _databaseProvider.GetConnection();
        
        var query = "SELECT * FROM NoteDetail as ND " +
                    "JOIN Note as N on N.NoteId = ND.NoteId " +
                    "JOIN [User] as U on U.UserId = N.UserId " +
                    "WHERE U.UserId = @UserId";
        
        return await connection.QueryAsync<NoteDetail>(query, new {UserId = userId}) ?? Enumerable.Empty<NoteDetail>();
    }

    public async Task<bool> DeleteNoteDetail(int noteDetailId)
    {
        var noteDetail = await  GetNoteDetailById(noteDetailId);

        if (noteDetail == null) 
            return false;
        
        _tableNoteDetails.Remove(noteDetail);
        
        return true;
    }

    private async Task<NoteDetail> GetNoteDetailById(int noteDetailId)
    {
        await using var connection = _databaseProvider.GetConnection();
        
        var query = "SELECT * FROM NoteDetail WHERE NoteDetailId = @NoteDetailId";
        
        return  await connection.QueryFirstOrDefaultAsync<NoteDetail>(query, new {NoteDetailId = noteDetailId});
    }
}