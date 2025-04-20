using HotelBackend;
using HotelBackend.Contracts;
using HotelBackend.Models;
using HotelBackend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// получение строки подключения из appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// игнорирование циклической сериализации объектов 
builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

// регистрация сервисов
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PasswordHasher<Client>>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IComfortService, ComfortService>();
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IRoomBookingService,  RoomBookingService>();
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<IRoomPaymentService, RoomPaymentService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped<IAmenityBookingService, AmenityBookingService>();
builder.Services.AddScoped<CleanupRoomBookingService>();
builder.Services.AddScoped<IHotelReviewService,  HotelReviewService>();

// регистрация фоновых задач
builder.Services.AddHostedService<CleanupRoomBookingService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


// регистрация frontend приложения
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});


// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowReactApp");

// Configure the HTTP request pipeline.

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
