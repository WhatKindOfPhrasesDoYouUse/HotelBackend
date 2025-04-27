using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IAmenityReviewService
    {
        Task<AmenityReview> SaveAmenityReview(AmenityReview amenityReview);
    }
}
