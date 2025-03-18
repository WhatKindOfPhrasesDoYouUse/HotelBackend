using HotelBackend.ValidationTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HotelBackend.Models;

[Table(name: "guest", Schema = "core")]
public partial class Guest
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "city_of_residence")]
    [Required(ErrorMessage = "Поле CityOfResidence модели Guest является обязательным")]
    [NameValidation]
    public string CityOfResidence { get; set; } = null!;

    [Column(name: "date_of_birth")]
    [Required(ErrorMessage = "Поле DateOfBirth модели Guest является обязательным")]
    [DataType(DataType.Date)]
/*    public DateOnly DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (value > DateOnly.FromDateTime(DateTime.Today))
            {
                throw new ArgumentOutOfRangeException(nameof(DateOfBirth), "Дата рождения не может быть в будущем.");
            }
            _dateOfBirth = value;
        }
    }
    private DateOnly _dateOfBirth;*/
    public DateTime DateOfBirth { get; set; }

    [Column(name: "passport_series_hash")]
    [Required(ErrorMessage = "Поле PassportSeriesHash модели Guest является обязательным")]
    [MaxLength(150, ErrorMessage = "Длина поля PassportSeriesHash модели Guest не может превышать 150 символов")]
    public string PassportSeriesHash { get; set; } = null!;

    [Column(name: "passport_number_hash")]
    [Required(ErrorMessage = "Поле PassportNumberHash модели Guest является обязательным")]
    [MaxLength(150, ErrorMessage = "Длина поля PassportNumberHash модели Guest не может превышать 150 символов")]
    public string PassportNumberHash { get; set; } = null!;

    [Column(name: "loyalty_status")]
    [Required(ErrorMessage = "Поле LoyaltyStatus модели Guest является обязательным")]
    [MaxLength(50, ErrorMessage = "Длина поля LoyaltyStatus модели Guest не может превышать 50 символов")]
    public string LoyaltyStatus { get; set; } = "Базовый";

    [Column(name: "card_id")]
    public long? CardId { get; set; }

    [ForeignKey(nameof(CardId))]
    public virtual Card? Card { get; set; }

    [Column(name: "client_id")]
    [Required(ErrorMessage = "Поле ClientId модели Guest является обязательным")]
    public long ClientId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public virtual Client? Client { get; set; } = null!;

    public virtual ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();

    public virtual ICollection<AmenityReview> AmenityReviews { get; set; } = new List<AmenityReview>();

    public virtual ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();
}
