using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IAmenityReviewService
    {
        Task<IEnumerable<AmenityReview>> GetAmenityReviewsByRoomId(long roomId);
        Task<PagedResult<AmenityReview>> GetAmenityReviewsByRoomIdPages(long roomId, int pageNumber = 1, int pageSize = 10);
        Task<AmenityReview> SaveAmenityReview(AmenityReview amenityReview);
    }
}
