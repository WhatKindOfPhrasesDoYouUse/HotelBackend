using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.DataTransferObjects
{
    public class AmenityBookingDto
    {
        public long? AmenityId { get; set; }
        public int? Quantity { get; set; }
        public long? GuestId { get; set; }
    }
}
