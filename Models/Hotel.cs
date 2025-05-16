using HotelBackend.ValidationTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "hotel", Schema = "core")]
public partial class Hotel
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Hotel является обязательным")]
    [NameValidation]
    public string Name { get; set; } = null!;

    [Column(name: "city")]
    [Required(ErrorMessage = "Поле City модели Hotel являеться обязательным")]
    [NameValidation]
    public string City { get; set; } = null!;

    [Column(name: "address")]
    [Required(ErrorMessage = "Поле Address модели Hotel является обзательным")]
    [MaxLength(100, ErrorMessage = "Длинна поля Address модели Hotel не может превышать 100 символов")]
    public string Address { get; set; } = null!;

    [Column(name: "description")]
    public string? Description { get; set; }

    [Column(name: "phone_number")]
    [Required(ErrorMessage = "Поле PhoneNumber модели Hotel являеться обязательным")]
    [PhoneNumberValidation]
    public string PhoneNumber { get; set; } = null!;

    [Column(name: "email")]
    [Required(ErrorMessage = "Поле Email модели Hotel являеться обязательным")]
    [EmailValidation]
    public string Email { get; set; } = null!;

    [Column(name: "year_of_construction")]
    public int? YearOfConstruction
    {
        get => _yearOfConstruction;
        set
        {
            if (value.HasValue && (value < 1300 || value > DateTime.Now.Year))
            {
                throw new ArgumentOutOfRangeException(nameof(YearOfConstruction),
                    $"Поле YearOfConstruction должно быть в интервале от 1300 до {DateTime.Now.Year}.");
            }
            _yearOfConstruction = value;
        }
    }
    private int? _yearOfConstruction;

    [Column("rating")]
    [Required(ErrorMessage = "Поле Rating модели Hotel являеться обязательным")]
    [Range(1, 5, ErrorMessage = "Поле Rating модели Hotel должен быть от 1 до 5")]
    public double Rating { get; set; } = 1;

    [Column(name: "hotel_type_id")]
    public long? HotelTypeId { get; set; }

    [ForeignKey(nameof(HotelTypeId))]
    public HotelType? HotelType { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
