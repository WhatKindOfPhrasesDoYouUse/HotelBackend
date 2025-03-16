using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая оплату за комнату.
/// </summary>
[Table(name: "room_payment", Schema = "core")]
public partial class RoomPayment
{
    /// <summary>
    /// Первичный ключ платежа за забронированную комнату.
    /// </summary>
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Дата платежа.
    /// </summary>
    [Column(name: "payment_date")]
    [Required(ErrorMessage = "Поле PaymentDate модели RoomPayment является обязательным")]
    public DateOnly PaymentDate { get; set; }

    /// <summary>
    /// Время платежа.
    /// </summary>
    [Column(name: "payment_time")]
    [Required(ErrorMessage = "Поле PaymentTime модели RoomPayment является обязательным")]
    public TimeOnly PaymentTime { get; set; }

    /// <summary>
    /// Общая стоимость платежа.
    /// </summary>
    [Column(name: "total_cost")]
    [Required(ErrorMessage = "Поле TotalCost модели RoomPayment является обязательным")]
    [Range(0, double.MaxValue, ErrorMessage = "Поле TotalAmount модели RoomPayment должна быть больше или равна 0.")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Статус платежа.
    /// </summary>
    [Column(name: "payment_status")]
    public string PaymentStatus { get; set; } = null!;

    /// <summary>
    /// Внешний ключ на тип платежа.
    /// </summary>
    [Column(name: "payment_type_id")]
    [Required(ErrorMessage = "Поле PaymentTypeId модели RoomPayment является обязательным")]
    public int PaymentTypeId { get; set; }

    /// <summary>
    /// Внешний ключ на бронирование комнаты.
    /// </summary>
    [Column(name: "room_booking_id")]
    [Required(ErrorMessage = "Поле RoomBookingId модели RoomPayment является обязательным")]
    public int RoomBookingId { get; set; }

    /// <summary>
    /// Связь с типом оплаты.
    /// </summary>
    [ForeignKey(nameof(PaymentType))]
    public virtual PaymentType PaymentType { get; set; } = null!;

    /// <summary>
    /// Связь с бронированием комнаты.
    /// </summary>
    [ForeignKey(nameof(RoomBookingId))]
    public virtual RoomBooking RoomBooking { get; set; } = null!;
}
