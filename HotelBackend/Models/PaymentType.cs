using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая тип оплаты в системе.
/// </summary>
[Table(name: "payment_type", Schema = "core")]
public partial class PaymentType
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
    /// Название типа оплаты.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели PaymentType являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Name модели PaymentType")]
    [MaxLength(50, ErrorMessage = "Длинна поля Name модели PaymentType не может превышать 50 символов")]
    public string Name { get; set; } = null!;


    /// <summary>
    /// Список платежей бронирования дополнительных услуг, которые относятся к данному типу.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество платежей, которые соответствуют этому типу.
    /// </remarks>
    public virtual ICollection<AmenityPayment> AmenityPayments { get; set; } = new List<AmenityPayment>();

    /// <summary>
    /// Список платежей бронирования комнат, которые относятся к данному типу.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество платежей, которые соответствуют этому типу.
    /// </remarks>
    public virtual ICollection<RoomPayment> RoomPayments { get; set; } = new List<RoomPayment>();
}
