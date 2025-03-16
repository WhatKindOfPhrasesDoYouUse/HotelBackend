using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

// TODO: TotalCost поменять тип int на double


/// <summary>
/// Модель, представляющая оплату за удобство.
/// </summary>
[Table(name: "amenity_payment", Schema = "core")]
public partial class AmenityPayment
{
    /// <summary>
    /// Первичный ключ платежа за забронированную услугу.
    /// </summary>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Дата платежа.
    /// </summary>
    [Column(name: "payment_date")]
    [Required(ErrorMessage = "Поле PaymentDate модели AmenityPayment является обязательным")]
    public DateOnly PaymentDate { get; set; }

    /// <summary>
    /// Время платежа.
    /// </summary>
    [Column(name: "payment_time")]
    [Required(ErrorMessage = "Поле PaymentTime модели AmenityPayment является обязательным")]
    public TimeOnly PaymentTime { get; set; } 

    /// <summary>
    /// Общая стоимость платежа.
    /// </summary>
    [Column(name: "total_cost")]
    [Required(ErrorMessage = "Поле TotalCost модели AmenityPayment является обязательным")]
    [Range(0, int.MaxValue, ErrorMessage = "Поле TotalCost модели AmenityPayment должна быть больше или равна 0.")]
    public int TotalCost { get; set; }

    /// <summary>
    /// Статус платежа.
    /// </summary>
    [Column(name: "payment_status")]
    public string? PaymentStatus { get; set; } = null;

    /// <summary>
    /// Внешний ключ на тип платежа.
    /// </summary>
    [Column(name: "payment_type_id")]
    [Required(ErrorMessage = "Поле PaymentTypeId модели AmenityPayment является обязательным")]
    public int PaymentTypeId { get; set; }

    /// <summary>
    /// Внешний ключ на бронирование удобства.
    /// </summary>
    [Column(name: "amenity_booking_id")]
    [Required(ErrorMessage = "Поле AmenityBookingId модели AmenityPayment является обязательным")]
    public int AmenityBookingId { get; set; }

    /// <summary>
    /// Связь с бронированием удобства.
    /// </summary>
    [ForeignKey(nameof(AmenityBookingId))]
    public virtual AmenityBooking AmenityBooking { get; set; } = null!;

    /// <summary>
    /// Связь с типом платежа.
    /// </summary>
    [ForeignKey(nameof(PaymentTypeId))]
    public virtual PaymentType PaymentType { get; set; } = null!;
}
