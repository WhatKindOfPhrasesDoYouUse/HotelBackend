using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IHotelReviewService
    {
        Task<IEnumerable<HotelReview>> GetHotelReviews();
        Task<HotelReview> GetHotelReviewByGuestId(long guestId);
        Task SaveHotelReview(HotelReview hotelReview);
    }
}
