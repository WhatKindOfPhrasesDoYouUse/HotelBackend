using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
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
        }

        [HttpDelete("{hotelReviewId:long}")]
        public async Task<IActionResult> DeleteHotelReviewById(long hotelReviewId)
        {
            try
            {
                await _hotelReviewService.DeleteHotelReviewById(hotelReviewId);
                return StatusCode(200, "Отзыв успешно удален");
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

        [HttpPatch("{hotelReviewId:long}")]
        public async Task<IActionResult> UpdateHotelReview(long hotelReviewId, HotelReview newHotelReview)
        {
            try
            {
                var hotelReview = await _hotelReviewService.UpdateHotelReview(hotelReviewId, newHotelReview);
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

        [HttpGet("paged")]
        public async Task<IActionResult> GetHotelReviewPages([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _hotelReviewService.GetHotelReviewPages(pageNumber, pageSize);
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
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }
    }
}
