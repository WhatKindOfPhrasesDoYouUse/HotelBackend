namespace HotelBackend.DataTransferObjects
{
    public class EmployeeDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Role { get; set;} = null!;
    }
}
