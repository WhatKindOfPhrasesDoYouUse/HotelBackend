using HotelBackend.DataTransferObjects;

namespace HotelBackend.Contracts
{
    public interface IAuthService
    {
        Task<string> Login(AuthDto authDto);
        Task<string> Registration(RegistrationGuestDto registrationGuestDto);
    }
}
