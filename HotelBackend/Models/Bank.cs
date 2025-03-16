using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая банк в системе.
/// </summary>
[Table(name: "bank", Schema = "core")]
public partial class Bank
{
    /// <summary>
    /// Первичный ключ банка.
    /// </summary>
    /// <remarks>
    /// Это поле является первичным ключом для таблицы.
    /// </remarks>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Название банка.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Bank являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Name модели Bank")]
    [MaxLength(50, ErrorMessage = "Длинна поля Name модели Bank не может превышать 50 символов")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Список карт, выпущенных этим банком.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество карт, связанных с этим банком.
    /// </remarks>
    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}