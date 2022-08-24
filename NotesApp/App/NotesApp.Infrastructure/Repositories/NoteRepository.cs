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


    public async Task<Note> GetNoteByUserId(int userId)
    {
        await using var connection = _databaseProvider.GetConnection();

        var query = "SELECT * FROM Note as N " +
                    "JOIN [User] as U on U.userId = N.UserId " +
                    "WHERE U.UserId = @UserId";
        
        return await connection.QueryFirstOrDefaultAsync<Note>(query, new {UserId = userId});

    }

    public async Task<NoteDetail> GetNoteDetailByNoteDetailIdAndUserId(int noteDetailId, int userId)
    {
        return  await GetNoteDetailByIdAndUserId(noteDetailId, userId);
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

    public async Task<bool> DeleteNoteDetail(int noteDetailId, int userId)
    {
        var noteDetail = await  GetNoteDetailByIdAndUserId(noteDetailId, userId);

        if (noteDetail == null) 
            return false;
        
        _tableNoteDetails.Remove(noteDetail);
        
        return true;
    }

    private async Task<NoteDetail> GetNoteDetailByIdAndUserId(int noteDetailId, int userId)
    {
        await using var connection = _databaseProvider.GetConnection();
        
        var query = "SELECT * FROM NoteDetail as ND " +
                            "JOIN Note as N on N.NoteId = ND.NoteId " +
                            "JOIN [User] as U on U.userId = N.UserId " +
                            "WHERE NoteDetailId = @NoteDetailId and U.UserId = @UserId";
        
        return  await connection.QueryFirstOrDefaultAsync<NoteDetail>(query, new {UserId = userId, NoteDetailId = noteDetailId});
    }
}