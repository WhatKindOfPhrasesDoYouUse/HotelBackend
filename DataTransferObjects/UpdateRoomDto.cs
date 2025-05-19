namespace HotelBackend.DataTransferObjects
{
    public class UpdateRoomDto
    {
        public int? RoomNumber { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public int? UnitPrice { get; set; }
        public long? HotelId { get; set; }
    }
}
