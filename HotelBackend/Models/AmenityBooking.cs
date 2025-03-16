using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая бронирование удобства для гостя.
/// </summary>
[Table(name: "amenity_booking", Schema = "core")]
public partial class AmenityBooking
{
    /// <summary>
    /// Первичный ключ бронирования дополнительной услуги.
    /// </summary>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    private DateOnly _orderDate;

    /// <summary>
    /// Пользовательская валидация для даты заказа.
    /// </summary>
    public static ValidationResult ValidateOrderDate(DateOnly value, ValidationContext context)
    {
        if (value > DateOnly.FromDateTime(DateTime.Now))
        {
            return new ValidationResult("Дата заказа не может быть позже текущей даты.");
        }
        return ValidationResult.Success;
    }

    /// <summary>
    /// Дата заказа удобства.
    /// </summary>
    /// <remarks>
    /// Дата заказа не может быть позже текущей даты.
    /// </remarks>
    [Column(name: "order_date")]
    [Required(ErrorMessage = "Поле OrderDate модели AmenityBooking является обязательным")]
    [CustomValidation(typeof(AmenityBooking), nameof(ValidateOrderDate))]
    public DateOnly OrderDate
    {
        get => _orderDate;
        set
        {
            if (value > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentOutOfRangeException(nameof(OrderDate),
                    "Дата заказа не может быть позже текущей даты.");
            }
            _orderDate = value;
        }
    }

    /// <summary>
    /// Время заказа дополнительной услуги.
    /// </summary>
    /// <remarks>
    /// Поле является обязательным.
    /// </remarks>
    [Column(name: "order_time")]
    [Required(ErrorMessage = "Поле OrderTime модели AmenityBooking является обязательным")]
    public TimeOnly OrderTime { get; set; }

    /// <summary>
    /// Дата готовности забронированной услуги.
    /// </summary>
    /// <remarks>
    /// Дата готовности не может быть раньше даты заказа.
    /// </remarks>
    [Column(name: "ready_date")]
    [Required(ErrorMessage = "Поле ReadyDate модели AmenityBooking является обязательным")]
    public DateOnly ReadyDate { get; set; }

    /// <summary>
    /// Время готовности забронированной услуги.
    /// </summary>
    /// <remarks>
    /// Поле является обязательным.
    /// </remarks>
    [Column(name: "ready_time")]
    [Required(ErrorMessage = "Поле ReadyTime модели AmenityBooking является обязательным")]
    public TimeOnly ReadyTime { get; set; }

    /// <summary>
    /// Статус выполнения заказа.
    /// </summary>
    /// <remarks>
    /// Поле обязательно и по умолчанию имеет значение "Ожидается подтверждение".
    /// </remarks>
    [Column(name: "completion_status")]
    [Required(ErrorMessage = "Поле CompletionStatus модели AmenityBooking является обязательным")]
    public string CompletionStatus { get; set; } = "В ожидании подтверждения";

    [Column(name: "quantity")]
    [Required(ErrorMessage = "Поле Quantity модели Room является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле Quantity модели Room должно быть больше 0")]
    public int Quantity { get; set; }

    /// <summary>
    /// Внешний ключ на удобство.
    /// </summary>
    [Column(name: "amenity_id")]
    [Required(ErrorMessage = "Поле AmenityId модели AmenityBooking является обязательным")]
    public int AmenityId { get; set; }

    /// <summary>
    /// Внешний ключ на гостя.
    /// </summary>
    [Column(name: "guest_id")]
    [Required(ErrorMessage = "Поле GuestId модели AmenityBooking является обязательным")]
    public int GuestId { get; set; }

    /// <summary>
    /// Внешний ключ на сотрудника.
    /// </summary>
    [Column(name: "employee_id")]
    public int? EmployeeId { get; set; }

    /// <summary>
    /// Связь с удобством.
    /// </summary>
    [ForeignKey(nameof(AmenityId))]
    [InverseProperty("AmenityBookings")]
    public virtual Amenity Amenity { get; set; } = null!;

    /// <summary>
    /// Связь с гостем.
    /// </summary>
    [ForeignKey(nameof(GuestId))]
    [InverseProperty("AmenityBookings")]
    public virtual Guest Guest { get; set; } = null!;

    /// <summary>
    /// Связь с сотрудником.
    /// </summary>
    [ForeignKey(nameof(EmployeeId))]
    [InverseProperty("AmenityBookings")]
    public virtual Employee Employee { get; set; } = null!;

    /// <summary>
    /// Список платежей за удобства.
    /// </summary>
    public virtual ICollection<AmenityPayment> AmenityPayments { get; set; } = new List<AmenityPayment>();
}
