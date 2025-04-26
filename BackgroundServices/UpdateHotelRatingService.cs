using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.BackgroundServices
{
    public class UpdateHotelRatingService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UpdateHotelRatingService(IServiceScopeFactory serviceScopeFactory) => this._serviceScopeFactory = serviceScopeFactory;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await UpdateHotelRating(stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }

        private async Task UpdateHotelRating(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                double totalRating = 0;

                var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var hotelReviews = await _context.HotelReviews.ToListAsync();

                if (hotelReviews.Any())
                {
                    var hotel = await _context.Hotels.FindAsync(1);

                    if (hotel == null)
                    {
                        return;
                    }

                    foreach (var hotelReview in hotelReviews)
                    {
                        totalRating += hotelReview.Rating;
                    }

                    hotel.Rating = totalRating / hotelReviews.Count();

                    _context.Hotels.Update(hotel);
                    _context.Entry(hotel).State = EntityState.Modified;
                    await _context.SaveChangesAsync(stoppingToken);
                }
            }
        }
    }
}
