using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;


[Table(name: "amenity_booking", Schema = "core")]
public partial class AmenityBooking
{

    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    private DateOnly _orderDate;

    public static ValidationResult ValidateOrderDate(DateOnly value, ValidationContext context)
    {
        if (value > DateOnly.FromDateTime(DateTime.Now))
        {
            return new ValidationResult("Дата заказа не может быть позже текущей даты.");
        }
        return ValidationResult.Success;
    }

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


    [Column(name: "order_time")]
    [Required(ErrorMessage = "Поле OrderTime модели AmenityBooking является обязательным")]
    public TimeOnly OrderTime { get; set; }

    [Column(name: "ready_date")]
    [Required(ErrorMessage = "Поле ReadyDate модели AmenityBooking является обязательным")]
    public DateOnly ReadyDate { get; set; }

    [Column(name: "ready_time")]
    [Required(ErrorMessage = "Поле ReadyTime модели AmenityBooking является обязательным")]
    public TimeOnly ReadyTime { get; set; }


    [Column(name: "completion_status")]
    [Required(ErrorMessage = "Поле CompletionStatus модели AmenityBooking является обязательным")]
    public string CompletionStatus { get; set; } = "В ожидании подтверждения";

    [Column(name: "quantity")]
    [Required(ErrorMessage = "Поле Quantity модели Room является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Поле Quantity модели Room должно быть больше 0")]
    public int Quantity { get; set; }

    [Column(name: "amenity_id")]
    [Required(ErrorMessage = "Поле AmenityId модели AmenityBooking является обязательным")]
    public long AmenityId { get; set; }

    [Column(name: "guest_id")]
    [Required(ErrorMessage = "Поле GuestId модели AmenityBooking является обязательным")]
    public long GuestId { get; set; }

    [Column(name: "employee_id")]
    public long? EmployeeId { get; set; }

    [ForeignKey(nameof(AmenityId))]
    [InverseProperty("AmenityBookings")]
    public virtual Amenity Amenity { get; set; } = null!;

    [ForeignKey(nameof(GuestId))]
    public virtual Guest Guest { get; set; } = null!;


    [ForeignKey(nameof(EmployeeId))]
    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<AmenityPayment> AmenityPayments { get; set; } = new List<AmenityPayment>();
}
