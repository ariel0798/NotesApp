using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotesApp.Domain.Interfaces;

namespace NotesApp.ApplicationCore.Services;

public class BackgroundTasksService : BackgroundService
{
    private const int DaysToBeRemove = 30;
    private const int HoursToRunTask = 24;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public BackgroundTasksService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var expiredNotesDetails = await unitOfWork.Notes.GetExpiredNoteDetails(DaysToBeRemove);

            foreach (var noteDetail in expiredNotesDetails)
            {
                await unitOfWork.Notes.DeleteNoteDetailById(noteDetail.NoteDetailId);
                await unitOfWork.Save();
            }
            await Task.Delay(ConvertHoursToMilliseconds(HoursToRunTask), stoppingToken);
        }
        
    }

    private int ConvertHoursToMilliseconds(int hours) => hours * 60 * 60 * 1000;
}