using HotelBackend.ValidationTypes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "amenity", Schema = "core")]
public partial class Amenity
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Amenity являеться обязательным")]
    [NameValidation]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("unit_price")]
    [Required(ErrorMessage = "Поле UnitPrice модели Amenity являеться обязательным.")]
    [Range(0, int.MaxValue, ErrorMessage = "Цена поля UnitPrice модели Amenity не может быть отрицательной.")]
    public int UnitPrice { get; set; }


    [Column("employee_type_id")]
    public long? EmployeeTypeId { get; set; } = null!;


    [ForeignKey("EmployeeTypeId")]
    public virtual EmployeeType? EmployeeType { get; set; } = null!;


    [Column("room_id")]
    public long RoomId { get; set; }


    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; } = null!;


    public virtual ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();

    public virtual ICollection<AmenityReview> AmenityReviews { get; set; } = new List<AmenityReview>();
}
