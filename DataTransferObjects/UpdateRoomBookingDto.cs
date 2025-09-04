namespace HotelBackend.DataTransferObjects
{
    public class UpdateRoomBookingDto
    {
        public DateOnly? CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
        public TimeOnly? CheckInTime { get; set; }
        public TimeOnly? CheckOutTime { get; set; }
        public int NumberOfGuests { get; set; } = 1!;
    }
}
