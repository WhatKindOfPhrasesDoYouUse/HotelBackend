using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "room", Schema = "core")]
public partial class Room
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "room_number")]
    [Required(ErrorMessage = "Поле RoomNumber модели Room является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле RoomNumber модели Room должно быть больше 0")]
    public int RoomNumber { get; set; }

    [Column(name: "description")]
    public string? Description { get; set; }

    [Column(name: "capacity")]
    [Required(ErrorMessage = "Поле Capacity модели Room является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле Capacity модели Room должно быть больше 0")]
    public int Capacity { get; set; }

    [Column(name: "unit_price")]
    [Required(ErrorMessage = "Поле UnitPrice модели Room является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле UnitPrice модели Room должно быть больше 0")]
    public int UnitPrice { get; set; }

    [Column(name: "hotel_id")]
    [Required(ErrorMessage = "Поле HotelId модели Room является обязательным")]
    public long HotelId { get; set; }

    [Column(name: "HotelId")]
    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

    public virtual ICollection<RoomComfort> RoomComforts { get; set; } = new List<RoomComfort>();
}
