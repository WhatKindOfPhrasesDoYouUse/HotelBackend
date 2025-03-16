using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая отель.
/// </summary>
[Table(name: "hotel", Schema = "core")]
public partial class Hotel
{
    /// <summary>
    /// Первичный ключ отеля.
    /// </summary>
    /// <remarks>
    /// Это поле является первичным ключом для таблицы.
    /// </remarks>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Название отеля.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Hotel является обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Name модели Hotel")]
    [MaxLength(50, ErrorMessage = "Длинна поля Name модели Hotel не может превышать 50 символов")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Название города в котором находится отель.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "city")]
    [Required(ErrorMessage = "Поле City модели Hotel являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля City модели Hotel")]
    [MaxLength(50, ErrorMessage = "Длинна поля City модели Hotel не может превышать 50 символов")]
    public string City { get; set; } = null!;

    [Column(name: "address")]
    [Required(ErrorMessage = "Поле Address модели Hotel является обзательным")]
    [MaxLength(100, ErrorMessage = "Длинна поля Address модели Hotel не может превышать 100 символов")]
    public string Address { get; set; } = null!;

    /// <summary>
    /// Описание отеля (может быть пустым).
    /// </summary>
    [Column(name: "description")]
    public string? Description { get; set; }

    /// <summary>
    /// Номер телефона отеля.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно соответствовать формату телефонного номера.
    /// Длина не может превышать 12 символов.
    /// </remarks>
    [Column(name: "phone_number")]
    [Required(ErrorMessage = "Поле PhoneNumber модели Hotel являеться обязательным")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Неверный формат поля PhoneNumber модели Hotel")]
    [MaxLength(50, ErrorMessage = "Длинна поля PhoneNumber модели Hotel не может превышать 12 символов")]
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Электронная почта отеля.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно соответствовать формату email.
    /// Длина не может превышать 100 символов.
    /// </remarks>
    [Column(name: "email")]
    [Required(ErrorMessage = "Поле Email модели Hotel являеться обязательным")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Неверный формат поля Email модели Hotel")]
    [MaxLength(100, ErrorMessage = "Длинна поля Email модели Hotel не может превышать 100 символов")]
    public string Email { get; set; } = null!;

    private int? _yearOfConstruction;

    [Column(name: "year_of_construction")]
    public int? YearOfConstruction 
    {
        get => _yearOfConstruction;
        set
        {
            if (value.HasValue && (value < 1300 || value > DateTime.Now.Year))
            {
                throw new ArgumentOutOfRangeException(nameof(YearOfConstruction), 
                    $"Поле YearOfConstruction модели Hotel должно быть в интервале от 1300 до {DateTime.Now.Year}.");
            }
            _yearOfConstruction = value;
        }
    }

    /// <summary>
    /// Рейтинг отеля (от 1 до 5).
    /// </summary>
    [Column("rating")]
    [Required(ErrorMessage = "Поле Rating модели Hotel являеться обязательным")]
    [Range(1, 5, ErrorMessage = "Поле Rating модели Hotel должен быть от 1 до 5")]
    public double Rating { get; set; } = 1;

    /// <summary>
    /// Список сотрудников, работающих в отеле.
    /// </summary>
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    /// <summary>
    /// Список отзывов об отеле.
    /// </summary>
    public virtual ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();

    /// <summary>
    /// Список номеров, принадлежащих отелю.
    /// </summary>
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
