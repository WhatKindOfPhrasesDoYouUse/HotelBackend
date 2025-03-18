using HotelBackend.Models;

namespace HotelBackend.DataTransferObjects
{
    public class RegistrationGuestDto
    {
        public Client Client { get; set; } = null!;
        public Guest Guest { get; set; } = null!;
    }
}
