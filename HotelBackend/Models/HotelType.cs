using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая тип гостиницы в системе.
/// </summary>
[Table(name: "hotel_type", Schema = "core")]
public partial class HotelType
{
    /// <summary>
    /// Первичный ключ типа гостиницы.
    /// </summary>
    /// <remarks>
    /// Это поле является первичным ключом для таблицы.
    /// </remarks>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Название типа гостиницы.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели HotelType являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Name модели HotelType")]
    [MaxLength(50, ErrorMessage = "Длинна поля Name модели HotelType не может превышать 50 символов")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание типа гостиницы.
    /// </summary>
    /// <remarks>
    /// Поле может быть пустым, не имеет ограничения по длине.
    /// </remarks>
    [Column(name: "description")]
    public string? Description { get; set; }

    /// <summary>
    /// Список гостиниц, которые относятся к данному типу.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество гостиниц, которые соответствуют этому типу.
    /// </remarks>
    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
