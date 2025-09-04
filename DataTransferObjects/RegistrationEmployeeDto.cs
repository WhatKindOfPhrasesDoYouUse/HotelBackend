namespace HotelBackend.DataTransferObjects
{
    public class RegistrationEmployeeDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public long EmployeeTypeId { get; set; }
        public long HotelId { get; set; }
    }
}
