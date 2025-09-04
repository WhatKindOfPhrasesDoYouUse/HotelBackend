namespace HotelBackend.DataTransferObjects
{
    public class DetailAmenityBookingDto
    {
        public long Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public TimeOnly OrderTime { get; set; }
        public string CompletionStatus { get; set; } = null!;
        public int Quantity { get; set; }
        public string AmenityName { get; set; } = null!;
        public string GuestName { get; set; } = null!;
        public int RoomNumber { get; set; }
        public bool IsPayd { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
