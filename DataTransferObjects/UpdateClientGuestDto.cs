namespace HotelBackend.DataTransferObjects
{
    public class UpdateClientGuestDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CityOfResidence { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PassportSeriesHash { get; set; }
        public string? PassportNumberHash { get; set; }
        public string? LoyaltyStatus { get; set; }
    }
}
