namespace HotelBackend.DataTransferObjects
{
    public class RoomPaymentDto
    {
        public long? Id { get; set; }
        public long? RoomBookingId { get; set; }
        public long? PaymentTypeId { get; set; }
    }
}
