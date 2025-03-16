using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая банковскую карту в системе.
/// </summary>
[Table(name: "card", Schema = "core")]
public partial class Card
{
    /// <summary>
    /// Первичный ключ типа оплаты.
    /// </summary>
    /// <remarks>
    /// Это поле является первичным ключом для таблицы.
    /// </remarks>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Номер банковской карты.
    /// </summary>
    /// <remarks>
    /// Поле обязательно для заполнения.
    /// Должно содержать ровно 16 цифр.
    /// Должно быть уникальным.
    /// </remarks>
    [Column(name: "card_number")]
    [Required(ErrorMessage = "Поле CardNumber модели Card является обязательным")]
    [RegularExpression(@"^[0-9]{16}$", ErrorMessage = "Неверный формат поля CardNumber модели Card")]
    [MaxLength(16, ErrorMessage = "Длина поля CardNumber модели Card не может превышать 16 символов")]
    public string CardNumber { get; set; } = null!;

    /// <summary>
    /// Срок действия карты.
    /// </summary>
    /// <remarks>
    /// Поле обязательно для заполнения.
    /// Должно соответствовать формату MM/YY.
    /// </remarks>
    [Column(name: "card_date")]
    [Required(ErrorMessage = "Поле CardDate модели Card является обязательным")]
    [RegularExpression(@"^(0[1-9]|1[0-2])/[0-9]{2}$", ErrorMessage = "Неверный формат поля CardDate модели Card")]
    [MaxLength(5, ErrorMessage = "Длина поля CardDate модели Card не может превышать 5 символов")]
    public string CardDate { get; set; } = null!;

    /// <summary>
    /// Внешний ключ, связывающий карту с банком.
    /// </summary>
    /// <remarks>
    /// Указывает, к какому банку относится данная карта.
    /// При удалении банка все связанные карты также удаляются (ON DELETE CASCADE).
    /// </remarks>
    [Column(name: "bank_id")]
    [Required(ErrorMessage = "Поле BankId модели Card является обязательным")]
    public long BankId { get; set; }

    /// <summary>
    /// Навигационное свойство, представляющее банк, к которому привязана карта.
    /// </summary>
    /// <remarks>
    /// Связь с моделью Bank через внешний ключ bank_id.
    /// </remarks>
    public virtual Bank Bank { get; set; } = null!;

    /// <summary>
    /// Список гостей, использующих данную карту.
    /// </summary>
    /// <remarks>
    /// Коллекция гостей, которые привязали эту карту в системе.
    /// </remarks>
    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
