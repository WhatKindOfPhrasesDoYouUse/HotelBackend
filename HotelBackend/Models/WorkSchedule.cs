using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая рабочий график сотрудников.
/// </summary>
[Table(name: "work_schedule", Schema = "core")]
public partial class WorkSchedule
{
    /// <summary>
    /// Первичный ключ рабочего графика.
    /// </summary>
    /// <remarks>
    /// Это поле является первичным ключом для таблицы.
    /// </remarks>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Дата, на которую планируется рабочий график.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым.
    /// </remarks>
    [Column(name: "work_date")]
    [Required(ErrorMessage = "Поле WorkDate модели WorkSchedule является обязательным")]
    public DateOnly WorkDate { get; set; }

    /// <summary>
    /// Время начала работы для указанного дня.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым. Время должно быть в формате TIME.
    /// </remarks>
    [Column(name: "start_time")]
    [Required(ErrorMessage = "Поле StartTime модели WorkSchedule является обязательным")]
    public TimeOnly StartTime { get; set; }

    /// <summary>
    /// Время окончания работы для указанного дня.
    /// </summary>
    /// <remarks>
    /// Поле не может быть пустым. Время должно быть в формате TIME.
    /// </remarks>
    [Column(name: "end_time")]
    [Required(ErrorMessage = "Поле EndTime модели WorkSchedule является обязательным")]
    public TimeOnly EndTime { get; set; }

    /// <summary>
    /// Список сотрудников, работающих в указанный день по данному графику.
    /// </summary>
    /// <remarks>
    /// Коллекция может содержать множество сотрудников, работающих в определенный день.
    /// </remarks>
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
