using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context) => this._context = context;

        public async Task<Client> GetGuestByClientId(long clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id клиента не может быть отрицательным значением");
                }

                var client = await _context.Clients
                    .Include(c => c.Guests)
                    .FirstOrDefaultAsync(c => c.Id == clientId);

                if (client == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гостя с таким clientId: {clientId} не существует");
                }

                return client;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при получении гостя со стороны сервера", ex);
            }
        }
    }
}
