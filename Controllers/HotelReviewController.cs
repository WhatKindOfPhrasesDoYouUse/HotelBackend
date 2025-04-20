using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/hotel-reviews")]
    [ApiController]
    public class HotelReviewController : ControllerBase
    {
        private readonly IHotelReviewService _hotelReviewService;

        public HotelReviewController(IHotelReviewService hotelReviewService) => this._hotelReviewService = hotelReviewService;

        [HttpGet]
        public async Task<IActionResult> GetHotelReviews()
        {
            try
            {
                var hotelReviews = await _hotelReviewService.GetHotelReviews();
                return StatusCode(200, hotelReviews);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpGet("{guestId:long}")]
        public async Task<IActionResult> GetHotelReviews(long guestId)
        {
            try
            {
                var hotelReview = await _hotelReviewService.GetHotelReviewByGuestId(guestId);
                return StatusCode(200, hotelReview);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

/*        [HttpPost]
        public async Task<IActionResult> SaveHotelReview(HotelReview hotelReview)
        {
            try
            {
                await _hotelReviewService.SaveHotelReview(hotelReview);
                return StatusCode(200, hotelReview);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }*/
    }
}
