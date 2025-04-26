using Microsoft.EntityFrameworkCore;
using HotelBackend;

public class CleanupRoomBookingService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CleanupRoomBookingService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CleanupBookings(stoppingToken);
            await CancelUnpaidBookings(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }

    private async Task CleanupBookings(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.UtcNow;

            var bookingsToDelete = await _context.RoomBookings
                .Where(rb => !rb.IsConfirmed && rb.CreatedAt < now.AddMinutes(-15))
                .ToListAsync(stoppingToken);

            if (bookingsToDelete.Any())
            {
                _context.RoomBookings.RemoveRange(bookingsToDelete);
                await _context.SaveChangesAsync(stoppingToken);
            }
        }
    }

    private async Task CancelUnpaidBookings(CancellationToken stoppingToken)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var now = DateTime.UtcNow;

            var unpaidBookings = await _context.RoomBookings
                .Where(rb => rb.IsConfirmed &&
                             rb.ConfirmationTime != null &&
                             rb.ConfirmationTime < now.AddMinutes(-15) &&
                             !rb.RoomPayments.Any())
                .ToListAsync(stoppingToken);

            if (unpaidBookings.Any())
            {
                _context.RoomBookings.RemoveRange(unpaidBookings);
                await _context.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
