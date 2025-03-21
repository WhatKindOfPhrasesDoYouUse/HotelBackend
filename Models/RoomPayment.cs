using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

[Table(name: "room_payment", Schema = "core")]
public partial class RoomPayment
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Column(name: "payment_date")]
    [Required(ErrorMessage = "Поле PaymentDate модели RoomPayment является обязательным")]
    public DateOnly PaymentDate { get; set; }

    [Column(name: "payment_time")]
    [Required(ErrorMessage = "Поле PaymentTime модели RoomPayment является обязательным")]
    public TimeOnly PaymentTime { get; set; }

    [Column(name: "total_amount")]
    [Required(ErrorMessage = "Поле TotalAmount модели RoomPayment является обязательным")]
    [Range(0, double.MaxValue, ErrorMessage = "Поле TotalAmount модели RoomPayment должна быть больше или равна 0.")]
    public decimal TotalAmount { get; set; }

    [Column(name: "payment_status")]
    public string PaymentStatus { get; set; } = null!;

    [Column(name: "payment_type_id")]
    [Required(ErrorMessage = "Поле PaymentTypeId модели RoomPayment является обязательным")]
    public long PaymentTypeId { get; set; }

    [Column(name: "room_booking_id")]
    [Required(ErrorMessage = "Поле RoomBookingId модели RoomPayment является обязательным")]
    public long RoomBookingId { get; set; }

    [ForeignKey(nameof(PaymentTypeId))]
    public virtual PaymentType PaymentType { get; set; } = null!;

    [ForeignKey(nameof(RoomBookingId))]
    public virtual RoomBooking RoomBooking { get; set; } = null!;
}
