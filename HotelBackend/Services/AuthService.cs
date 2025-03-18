using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<Client> _passwordHasher;

        public AuthService(ApplicationDbContext context, IConfiguration configuration, PasswordHasher<Client> passwordHasher)
        {
            this._context = context;
            this._configuration = configuration;
            this._passwordHasher = passwordHasher;
        }

        private string GenerateJwtToken(Client client)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, client.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            using (var context = new ApplicationDbContext())
            {
                var isGuest = context.Guests.Any(g => g.ClientId == client.Id);

                if (isGuest)
                {
                    claims.Add(new Claim("role", "guest"));
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(120);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(Client client, string password)
        {
            return _passwordHasher.HashPassword(client, password);
        }

        public bool VerifyPassword(Client client, string password)
        {
            var verificationResult = _passwordHasher.VerifyHashedPassword(client, client.PasswordHash, password);
            return verificationResult == PasswordVerificationResult.Success;
        }

        public async Task<string> Login(AuthDto authDto)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == authDto.Email);

                if (client == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Пользователь с таким {authDto.Email} не найден");
                }

                if (!VerifyPassword(client, authDto.Password))
                {
                    throw new ServiceException(ErrorCode.Unauthorized, $"Пользователь c id {client.Id} не прошел верефикацию по паролю");
                }

                return GenerateJwtToken(client);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при аутентификации пользователя со стороны сервера", ex);
            }
        }
    }
}
