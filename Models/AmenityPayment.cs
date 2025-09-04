using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

// TODO: TotalCost поменять тип int на double

[Table(name: "amenity_payment", Schema = "core")]
public partial class AmenityPayment
{

    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "payment_date")]
    [Required(ErrorMessage = "Поле PaymentDate модели AmenityPayment является обязательным")]
    public DateOnly PaymentDate { get; set; }

    [Column(name: "payment_time")]
    [Required(ErrorMessage = "Поле PaymentTime модели AmenityPayment является обязательным")]
    public TimeOnly PaymentTime { get; set; }

    [Column(name: "total_amount")]
    [Required(ErrorMessage = "Поле TotalAmount модели AmenityPayment является обязательным")]
    [Range(0, double.MaxValue, ErrorMessage = "Поле TotalAmount модели AmenityPayment должна быть больше или равна 0.")]
    public decimal TotalAmount { get; set; }

    [Column(name: "payment_status")]
    public string? PaymentStatus { get; set; } = null;

    [Column(name: "payment_type_id")]
    [Required(ErrorMessage = "Поле PaymentTypeId модели AmenityPayment является обязательным")]
    public long PaymentTypeId { get; set; }

    [Column(name: "amenity_booking_id")]
    [Required(ErrorMessage = "Поле AmenityBookingId модели AmenityPayment является обязательным")]
    public long AmenityBookingId { get; set; }

    [ForeignKey(nameof(AmenityBookingId))]
    public virtual AmenityBooking AmenityBooking { get; set; } = null!;

    [ForeignKey(nameof(PaymentTypeId))]
    public virtual PaymentType PaymentType { get; set; } = null!;
}
