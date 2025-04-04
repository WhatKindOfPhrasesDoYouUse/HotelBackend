using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "card", Schema = "core")]
public partial class Card
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "card_number")]
    [Required(ErrorMessage = "Поле CardNumber модели Card является обязательным")]
    [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Неверный формат поля CardNumber модели Card")]
    [MaxLength(16, ErrorMessage = "Длина поля CardNumber модели Card не может превышать 16 символов")]
    public string CardNumber { get; set; } = null!;

    [Column(name: "card_date")]
    [Required(ErrorMessage = "Поле CardDate модели Card является обязательным")]
    [RegularExpression(@"^(0[1-9]|1[0-2])/[0-9]{2}$", ErrorMessage = "Неверный формат поля CardDate модели Card")]
    [MaxLength(5, ErrorMessage = "Длина поля CardDate модели Card не может превышать 5 символов")]
    public string CardDate { get; set; } = null!;

    [Column(name: "bank_id")]
    [Required(ErrorMessage = "Поле BankId модели Card является обязательным")]
    public long BankId { get; set; }

    [ForeignKey(nameof(BankId))]
    public virtual Bank? Bank { get; set; } = null!;

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
