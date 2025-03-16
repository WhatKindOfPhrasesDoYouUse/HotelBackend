using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая сотрудника в системе.
/// </summary>
[Table(name: "employee", Schema = "core")]
public partial class Employee
{
    /// <summary>
    /// Первичный ключ сотрудника.
    /// </summary>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Дата найма сотрудника.
    /// </summary>
    [Column(name: "hire_date")]
    [Required(ErrorMessage = "Поле HireDate модели Employee является обязательным")]
    [DataType(DataType.Date)]
    public DateOnly HireDate { get; set; }

    /// <summary>
    /// Базовая зарплата сотрудника.
    /// </summary>
    [Column(name: "base_salary")]
    [Required(ErrorMessage = "Поле BaseSalary модели Employee является обязательным")]
    [Range(0, 9999999.99, ErrorMessage = "Значение BaseSalary должно быть положительным числом и не превышать 9999999.99")]
    public decimal BaseSalary { get; set; }

    public long HotelId { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    /// <summary>
    /// Идентификатор типа сотрудника.
    /// </summary>
    [Column(name: "employee_type_id")]
    [Required(ErrorMessage = "Поле EmployeeTypeId модели Employee является обязательным")]
    public int EmployeeTypeId { get; set; }

    /// <summary>
    /// Идентификатор графика работы сотрудника (может быть NULL).
    /// </summary>
    [Column(name: "work_schedule_id")]
    public long? WorkScheduleId { get; set; }

    /// <summary>
    /// Идентификатор клиента, к которому привязан сотрудник.
    /// </summary>
    [Column(name: "client_id")]
    [Required(ErrorMessage = "Поле ClientId модели Employee является обязательным")]
    public long ClientId { get; set; }

    /// <summary>
    /// Связь с клиентом.
    /// </summary>
    [ForeignKey("ClientId")]
    public virtual Client Client { get; set; } = null!;

    /// <summary>
    /// Связь с типом сотрудника.
    /// </summary>
    [ForeignKey("EmployeeTypeId")]
    public virtual EmployeeType EmployeeType { get; set; } = null!;

    /// <summary>
    /// Связь с графиком работы (может быть NULL).
    /// </summary>
    [ForeignKey("WorkScheduleId")]
    public virtual WorkSchedule? WorkSchedule { get; set; }

    /// <summary>
    /// Список бронирований, которые принадлежат этому сотруднику.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать нескольких бронирований дополнительных услуг, которые связаны с данным сотрудником.
    /// </remarks>
    public virtual ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();
}
