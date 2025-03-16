using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая тип комфорта системе.
/// </summary>
public partial class Comfort
{
    /// <summary>
    /// Первичный ключ типа комфорта.
    /// </summary>
    /// <remarks>
    /// Это поле является первичным ключом для таблицы.
    /// </remarks>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Название типа комфорта.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Comfort являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Name модели Comfort")]
    [MaxLength(50, ErrorMessage = "Длинна поля Name модели Comfort не может превышать 50 символов")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Список комнат, которые имеют данный тип комфорта.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество комнат, которые обладают данным типом комфорта.
    /// </remarks>
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
