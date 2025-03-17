using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HotelBackend.ValidationTypes;

namespace HotelBackend.Models;

[Table(name: "payment_type", Schema = "core")]
public partial class PaymentType
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели PaymentType являеться обязательным")]
    [NameValidation]
    public string Name { get; set; } = null!;

    public virtual ICollection<AmenityPayment> AmenityPayments { get; set; } = new List<AmenityPayment>();

    public virtual ICollection<RoomPayment> RoomPayments { get; set; } = new List<RoomPayment>();
}
