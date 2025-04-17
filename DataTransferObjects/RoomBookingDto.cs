namespace HotelBackend.DataTransferObjects
{
    public class RoomBookingDto
    {
        public long? RoomBookingId { get; set; }
        public DateOnly? CheckInDate { get; set; }
        public DateOnly? CheckOutDate { get; set; }
        public TimeOnly? CheckInTime { get; set; }
        public TimeOnly? CheckOutTime { get; set; }
        public DateOnly? CancelUntilDate { get; set; }
        public TimeOnly? CancelUntilTime { get; set; }
        public int? NumberOfGuests { get; set; }
        public int? Capacity { get; set; }
        public long? UnitPrice { get; set; }
        public int? RoomNumber { get; set; }
        public bool? IsPayd { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsConfirmed { get; set; }
        public DateTime? ConfirmationTime { get; set; }
    }
}