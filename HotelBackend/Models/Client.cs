using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая клиента в системе. 
/// </summary>
[Table(name: "client", Schema = "core")]
public partial class Client
{
    /// <summary>
    /// Первичный ключ клиента.
    /// </summary>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Имя клиента.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели Client являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Name модели Client ")]
    [MaxLength(50, ErrorMessage = "Длинна поля Name модели Client не может превышать 50 символов")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Фамилия клиента.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "surname")]
    [Required(ErrorMessage = "Поле Surname модели Client являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Surname модели Client")]
    [MaxLength(50, ErrorMessage = "Длинна поля Surname модели Client не может превышать 50 символов")]
    public string Surname { get; set; } = null!;

    /// <summary>
    /// Отчество клиента.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно содержать только буквы (русские или латинские) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "patronymic")]
    [Required(ErrorMessage = "Поле Patronymic модели Client являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Patronymic модели Client")]
    [MaxLength(50, ErrorMessage = "Длинна поля Patronymic модели Client не может превышать 50 символов")]
    public string Patronymic { get; set; } = null!;

    /// <summary>
    /// Электронная почта клиента.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно соответствовать формату email.
    /// Длина не может превышать 100 символов.
    /// </remarks>
    [Column(name: "email")]
    [Required(ErrorMessage = "Поле Email модели Client являеться обязательным")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Неверный формат поля Email модели Client")]
    [MaxLength(100, ErrorMessage = "Длинна поля Email модели Client не может превышать 100 символов")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Номер телефона клиента.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, должно соответствовать формату телефонного номера.
    /// Длина не может превышать 12 символов.
    /// </remarks>
    [Column(name: "phone_number")]
    [Required(ErrorMessage = "Поле PhoneNumber модели Client являеться обязательным")]
    [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Неверный формат поля PhoneNumber модели Client")]
    [MaxLength(50, ErrorMessage = "Длинна поля PhoneNumber модели Client не может превышать 12 символов")]
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Хэш пароля клиента.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым, длина не может превышать 150 символов.
    /// </remarks>
    [Column(name: "password_hash")]
    [Required(ErrorMessage = "Поле PasswordHash модели Client являеться обязательным")]
    [MaxLength(150, ErrorMessage = "Длинна поля PasswordHash модели Client не может превышать 150 символов")]
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Список сотрудников, связанных с этим клиентом.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество сотрудников, работающих с данным клиентом.
    /// </remarks>
    [InverseProperty("Client")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    /// <summary>
    /// Список гостей, связанных с этим клиентом.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество гостей, зарегистрированных на имя клиента.
    /// </remarks>
    [InverseProperty("Client")]
    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}