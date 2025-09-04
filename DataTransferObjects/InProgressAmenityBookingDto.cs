namespace HotelBackend.DataTransferObjects
{
    public class InProgressAmenityBookingDto
    {
        public long Id { get; set; }
        public DateOnly OrderDate { get; set; }
        public TimeOnly OrderTime { get; set; }
        public DateOnly ReadyDate { get; set; }
        public TimeOnly ReadyTime { get; set; }
        public string CompletionStatus { get; set; } = null!;
        public int Quantity { get; set; }
        public string AmenityName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public bool IsPayd { get; set; }
        public string GuestName { get; set; } = null!;
        public string RoomBookingId { get; set; } = null!;
        public int RoomNumber { get; set; }
    }
}
