using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представления типа сотрудника в системе.
/// </summary>
[Table(name: "employee_type", Schema = "core")]
public partial class EmployeeType
{
    /// <summary>
    /// Первичный ключ типа сотрудника.
    /// </summary>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Название типа сотрудника.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым. Оно должно содержать только буквы (латинские или кириллические) и дефис.
    /// Длина не может превышать 50 символов.
    /// </remarks>
    [Column(name: "name")]
    [Required(ErrorMessage = "Поле Name модели EmployeeType являеться обязательным")]
    [RegularExpression(@"^[A-Za-zА-Яа-я\-]+$", ErrorMessage = "Неверный формат поля Name модели EmployeeType")]
    [MaxLength(50, ErrorMessage = "Длинна поля Name модели EmployeeType не может превышать 50 символов")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Список сотрудников, которые принадлежат этому типу.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать нескольких сотрудников, которые связаны с данным типом.
    /// </remarks>
    [InverseProperty("EmployeeType")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
}
