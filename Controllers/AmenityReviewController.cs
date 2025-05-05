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

        [HttpGet("{roomId:long}/room")]
        public async Task<IActionResult> GetAmenityReviewsByRoomId(long roomId)
        {
            try
            {
                var amenityReviews = await _amenityReviewService.GetAmenityReviewsByRoomId(roomId);
                return StatusCode(200, amenityReviews);
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

        [HttpGet("{roomId:long}/room/paged")]
        public async Task<IActionResult> GetAmenityReviewsByRoomIdPages(long roomId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _amenityReviewService.GetAmenityReviewsByRoomIdPages(roomId, pageNumber, pageSize);
                var totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);

                var response = new
                {
                    items = result.Items,
                    pageNumber = result.PageNumber,
                    pageSize = result.PageSize,
                    totalCount = result.TotalCount,
                    totalPages = totalPages
                };

                return StatusCode(200, response);
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
