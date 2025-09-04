using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HotelBackend.ValidationTypes;

namespace HotelBackend.Models;

[Table(name: "comfort", Schema = "core")]
public partial class Comfort
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Comfort являеться обязательным")]
    [NameValidation]
    public string Name { get; set; } = null!;

    public virtual ICollection<RoomComfort> RoomComforts { get; set; } = new List<RoomComfort>();
}
