namespace NotesApp.Infrastructure.Services.Interfaces;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime AddDays(int days);
    DateTime AddMinutes(int minutes);
}