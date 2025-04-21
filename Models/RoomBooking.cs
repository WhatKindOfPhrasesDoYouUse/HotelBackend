using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;


[Table(name: "room_booking", Schema = "core")]
public partial class RoomBooking
{
    [Column(name: "id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column(name: "check_in_date")]
    [Required(ErrorMessage = "Поле CheckInDate модели RoomBooking является обязательным")]
    public DateOnly CheckInDate { get; set; }

    [Column(name: "check_out_date")]
    [Required(ErrorMessage = "Поле CheckOutDate модели RoomBooking является обязательным")]
    public DateOnly CheckOutDate { get; set; }

    [Column(name: "check_in_time")]
    [Required(ErrorMessage = "Поле CheckInTime модели RoomBooking является обязательным")]
    public TimeOnly CheckInTime { get; set; }

    [Column(name: "check_out_time")]
    [Required(ErrorMessage = "Поле CheckOutTime модели RoomBooking является обязательным")]
    public TimeOnly CheckOutTime { get; set; }

    [Column(name: "number_of_guests")]
    [Required(ErrorMessage = "Поле NumberOfGuests модели RoomBooking является обязательным")]
    [Range(1, int.MaxValue, ErrorMessage = "Количество гостей должно быть больше 0")]
    public int NumberOfGuests { get; set; }

    [Column(name: "created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column(name: "is_confirmed")]
    public bool IsConfirmed { get; set; } = false;

    [Column(name: "confirmation_time")]
    public DateTime? ConfirmationTime { get; set; }

    [Column(name: "guest_id")]
    [Required(ErrorMessage = "Поле QuestId модели RoomBooking является обязательным")]
    public long GuestId { get; set; }

    [Column(name: "room_id")]
    [Required(ErrorMessage = "Поле RoomId модели RoomBooking является обязательным")]
    public long RoomId { get; set; }

    [Column(nameof(GuestId))]
    public virtual Guest? Guest { get; set; } = null!;

    [Column(nameof(RoomId))]
    public virtual Room? Room { get; set; } = null!;
    public virtual HotelReview? HotelReview { get; set; } = null!;
    public virtual ICollection<RoomPayment> RoomPayments { get; set; } = new List<RoomPayment>();
    public virtual ICollection<AdditionalGuest> AdditionalGuests { get; set; } = new List<AdditionalGuest>();
}
