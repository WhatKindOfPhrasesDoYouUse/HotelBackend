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
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("client_id", client.Id.ToString()),
                new Claim("client_name", client.Name.ToString())
            };

            using (var context = new ApplicationDbContext())
            {
                var isGuest = context.Guests.Any(g => g.ClientId == client.Id);

                if (isGuest)
                {
                    claims.Add(new Claim("role", "guest"));
                }

                if (!isGuest)
                {
                    var employee = _context.Employees
                        .Include(et => et.EmployeeType)
                        .FirstOrDefault(e => e.ClientId == client.Id);

                    if (employee != null && employee.EmployeeType != null) 
                    {
                        claims.Add(new Claim("role", "employee"));
                        claims.Add(new Claim("role", employee.EmployeeType.Name ?? string.Empty));
                    }
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

/*                if (!VerifyPassword(client, authDto.Password))
                {
                    throw new ServiceException(ErrorCode.Unauthorized, $"Пользователь c id {client.Id} не прошел верефикацию по паролю");
                }*/

                return GenerateJwtToken(client);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при аутентификации пользователя со стороны сервера", ex);
            }
        }

        public async Task<string> RegistrationGuest(RegistrationGuestDto registrationGuestDto)
        {
            if (registrationGuestDto == null)
            {
                throw new ServiceException(ErrorCode.BadRequest, "Отсутствует информация о госте");
            }

            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Email == registrationGuestDto.Email);

            if (existingClient != null)
            {
                throw new ServiceException(ErrorCode.BadRequest, "Клиент с таким email уже существует");
            }

            try
            {
                Client client = new Client()
                {
                    Name = registrationGuestDto.Name,
                    Surname = registrationGuestDto.Surname,
                    Patronymic = registrationGuestDto.Patronymic,
                    Email = registrationGuestDto.Email,
                    PhoneNumber = registrationGuestDto.PhoneNumber,
                    PasswordHash = ""
                };

                client.PasswordHash = HashPassword(client, registrationGuestDto.PasswordHash);

                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();

                Guest guest = new Guest()
                {
                    CityOfResidence = registrationGuestDto.CityOfResidence,
                    DateOfBirth = registrationGuestDto.DateOfBirth.Kind == DateTimeKind.Unspecified
                        ? DateTime.SpecifyKind(registrationGuestDto.DateOfBirth, DateTimeKind.Utc)
                        : registrationGuestDto.DateOfBirth.ToUniversalTime(),
                    PassportSeriesHash = registrationGuestDto.PassportSeriesHash,
                    PassportNumberHash = registrationGuestDto.PassportNumberHash,
                    ClientId = client.Id
                };

                await _context.Guests.AddAsync(guest);
                await _context.SaveChangesAsync();

                return "Регистрация прошла успешно";
            }
            catch (ServiceException ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Ошибка при регистрации", ex);
            }
        }

        public async Task<string> RegistrationEmployee(RegistrationEmployeeDto registrationEmployeeDto)
        {
            if (registrationEmployeeDto == null)
            {
                throw new ServiceException(ErrorCode.BadRequest, "Отсутствует информация о работнике");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingClient = await _context.Clients
                    .FirstOrDefaultAsync(c => c.Email == registrationEmployeeDto.Email || c.PhoneNumber == registrationEmployeeDto.PhoneNumber);

                if (existingClient != null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Клиент с таким Email или PhoneNumber уже существует");
                }

                Client client = new Client
                {
                    Name = registrationEmployeeDto.Name,
                    Surname = registrationEmployeeDto.Surname,
                    Patronymic = registrationEmployeeDto.Patronymic,
                    PhoneNumber = registrationEmployeeDto.PhoneNumber,
                    Email = registrationEmployeeDto.Email,
                    PasswordHash = ""
                };

                client.PasswordHash = HashPassword(client, registrationEmployeeDto.PasswordHash);

                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();

                Employee employee = new Employee
                {
                    HotelId = 1,
                    ClientId = client.Id,
                    EmployeeTypeId = registrationEmployeeDto.EmployeeTypeId
                };

                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return "Регистрация сотрудника успешно прошла";
            }
            catch (ServiceException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ClientVerification(VerificationDto verificationDto)
        {
            try
            {
                if (verificationDto.ClientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id клиента не может быть нулем или отрицательным числом");
                }

                var client = await _context.Clients.FindAsync(verificationDto.ClientId);

                if (client == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Клиент с ID {verificationDto.ClientId} не найден");
                }

                return VerifyPassword(client, verificationDto.Password);
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*public async Task<string> Registration(RegistrationGuestDto registrationGuestDto)
        {
            if (registrationGuestDto.Client == null || registrationGuestDto.Guest == null)
            {
                throw new ServiceException(ErrorCode.BadRequest, "Отсутствует информация о клиенте (и/или) госте");
            }

            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Email == registrationGuestDto.Client.Email);

            if (existingClient != null)
            {
                throw new ServiceException(ErrorCode.BadRequest, "Клиент с таким email уже существует");
            }

            try
            {
                Client client = new Client()
                {
                    Name = registrationGuestDto.Client.Name,
                    Surname = registrationGuestDto.Client.Surname,
                    Patronymic = registrationGuestDto.Client.Patronymic,
                    Email = registrationGuestDto.Client.Email,
                    PhoneNumber = registrationGuestDto.Client.PhoneNumber,
                    PasswordHash = HashPassword(registrationGuestDto.Client, registrationGuestDto.Client.PasswordHash)
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                Guest guest = new Guest()
                {
                    CityOfResidence = registrationGuestDto.Guest.CityOfResidence,
                    DateOfBirth = registrationGuestDto.Guest.DateOfBirth.Kind == DateTimeKind.Unspecified
                        ? DateTime.SpecifyKind(registrationGuestDto.Guest.DateOfBirth, DateTimeKind.Utc)
                        : registrationGuestDto.Guest.DateOfBirth.ToUniversalTime(),
                    PassportSeriesHash = registrationGuestDto.Guest.PassportSeriesHash,
                    PassportNumberHash = registrationGuestDto.Guest.PassportNumberHash,
                    LoyaltyStatus = registrationGuestDto.Guest.LoyaltyStatus,
                    ClientId = client.Id
                };

                _context.Guests.Add(guest);
                await _context.SaveChangesAsync();

                return $"{client.ToString()} {guest.ToString()}";
            }
            catch (ServiceException ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Ошибка при регистрации", ex);
            }
        }*/
    }
}
