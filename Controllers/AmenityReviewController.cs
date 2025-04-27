using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/amenity-reviews")]
    [ApiController]
    public class AmenityReviewController : ControllerBase
    {
        private IAmenityReviewService _amenityReviewService;

        public AmenityReviewController(IAmenityReviewService amenityReviewService) => this._amenityReviewService = amenityReviewService;

        [HttpPost]
        public async Task<IActionResult> SaveAmenityReview(AmenityReview amenityReview)
        {
            try
            {
                var newAmenityReview = await _amenityReviewService.SaveAmenityReview(amenityReview);
                return StatusCode(200, newAmenityReview);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
