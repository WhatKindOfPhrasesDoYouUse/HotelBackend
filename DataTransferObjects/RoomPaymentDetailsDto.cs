using HotelBackend.Models;

namespace HotelBackend.DataTransferObjects
{
    public class RoomPaymentDetailsDto
    {
        public string? HotelName { get; set; }
        public int? NumberRoom { get; set; }
        public decimal? TotalAmount { get; set; }
        public RoomBooking? RoomBooking { get; set; }
        public Card? Card { get; set; }
    }
}
