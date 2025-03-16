using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая дополнительную услугу в гостинице.
/// </summary>
[Table(name: "amenity", Schema = "core")]
public partial class Amenity
{
    /// <summary>
    /// Первичный ключ дополнительной услуги.
    /// </summary>
    /// <remarks>
    /// Это поле является первичным ключом для таблицы.
    /// </remarks>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Название дополнительной услуги.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Amenity являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Name модели Amenity")]
    [MaxLength(50, ErrorMessage = "Длинна поля Name модели Amenity не может превышать 50 символов")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Описание дополнительной услуги.
    /// </summary>
    /// <remarks>
    /// Поле может быть пустым. Максимальная длина - 1000 символов.
    /// </remarks>
    [Column("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Цена услуги за единицу.
    /// </summary>
    /// <remarks>
    /// Поле является обязательным. Значение не может быть отрицательным.
    /// </remarks>
    [Column("unit_price")]
    [Required(ErrorMessage = "Поле UnitPrice модели Amenity являеться обязательным.")]
    [Range(0, int.MaxValue, ErrorMessage = "Цена поля UnitPrice модели Amenity не может быть отрицательной.")]
    public int UnitPrice { get; set; }

    /// <summary>
    /// Тип сотрудника, предоставляющего услугу.
    /// </summary>
    /// <remarks>
    /// Поле может быть пустым. Если тип сотрудника удален, значение устанавливается в NULL.
    /// </remarks>
    [Column("employee_type_id")]
    public long? EmployeeTypeId { get; set; } = null!;

    /// <summary>
    /// Навигационное свойство для типа сотрудника.
    /// </summary>
    [ForeignKey("EmployeeTypeId")]
    public virtual EmployeeType? EmployeeType { get; set; } = null!;

    /// <summary>
    /// Номер, к которому привязана услуга.
    /// </summary>
    /// <remarks>
    /// Поле может быть пустым. Если номер удален, значение устанавливается в NULL.
    /// </remarks>
    [Column("room_id")]
    public int RoomId { get; set; }

    /// <summary>
    /// Навигационное свойство для номера.
    /// </summary>
    [ForeignKey("RoomId")]
    public virtual Room Room { get; set; } = null!;

    /// <summary>
    /// Список бронирований, связанных с данной услугой.
    /// </summary>
    public virtual ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();

    /// <summary>
    /// Список отзывов, связанных с данной услугой.
    /// </summary>
    public virtual ICollection<AmenityReview> AmenityReviews { get; set; } = new List<AmenityReview>();
}
