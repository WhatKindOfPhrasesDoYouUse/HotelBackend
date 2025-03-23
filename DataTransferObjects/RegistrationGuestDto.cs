using HotelBackend.Models;

namespace HotelBackend.DataTransferObjects
{
    public class RegistrationGuestDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string CityOfResidence { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PassportSeriesHash { get; set; } = null!;
        public string PassportNumberHash { get; set; } = null!;
    }
}
