using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IHotelReviewService
    {
        Task<IEnumerable<HotelReview>> GetHotelReviews();
        Task<HotelReview> GetHotelReviewByGuestId(long guestId);
        Task SaveHotelReview(HotelReview hotelReview);
        Task DeleteHotelReviewById(long hotelReviewId);
        Task<HotelReview> UpdateHotelReview(long hotelReviewId, HotelReview newHotelReview);
        Task<PagedResult<HotelReview>> GetHotelReviewPages(int pageNumber = 1, int pageSize = 10);
        Task<PagedResult<HotelReview>> GetHotelReviewPagesByGuestId(long guestId, int pageNumber = 1, int pageSize = 10);
        Task<int> GetHotelReviewCount();
        Task<double> GetHotelReviewAvgRating();
    }
}
