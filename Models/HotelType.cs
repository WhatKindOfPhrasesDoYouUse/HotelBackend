using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HotelBackend.ValidationTypes;

namespace HotelBackend.Models;

[Table(name: "hotel_type", Schema = "core")]
public partial class HotelType
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели HotelType являеться обязательным")]
    [NameValidation]
    public string Name { get; set; } = null!;

    [Column(name: "description")]
    public string? Description { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
