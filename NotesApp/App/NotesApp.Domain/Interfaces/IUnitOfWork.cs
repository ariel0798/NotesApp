namespace NotesApp.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    INoteRepository Notes { get; }
    Task Save();
}