namespace HotelBackend.DataTransferObjects
{
    public class AmenityPaymentDto
    {
        public long? Id { get; set; }
        public long? AmenityBookingId { get; set;}
        public long? PaymentTypeId { get; set; }
    }
}
