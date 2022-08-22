using NotesApp.Domain.Interfaces;
using NotesApp.Infrastructure.Context;
using NotesApp.Infrastructure.DatabaseProvider;

namespace NotesApp.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly NotesAppDbContext _context;
    private readonly IDatabaseProvider _databaseProvider;
    private IUserRepository _users;
    private INoteRepository _notes;
    
    
    public UnitOfWork(NotesAppDbContext context, IDatabaseProvider databaseProvider)
    {
        _context = context;
        _databaseProvider = databaseProvider;
    }

    public IUserRepository Users => _users ?? new UserRepository(_context, _databaseProvider);
    public INoteRepository Notes => _notes ?? new NoteRepository(_context, _databaseProvider);
    
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}