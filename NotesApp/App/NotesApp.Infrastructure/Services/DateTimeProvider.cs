using NotesApp.Infrastructure.Services.Interfaces;

namespace NotesApp.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;

    public DateTime AddDays(int days) => DateTime.Now.AddDays(days);

    public DateTime AddMinutes(int minutes) => DateTime.Now.AddMinutes(minutes);
}