using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBackend.Models;

/// <summary>
/// Модель, представляющая бронирования номера.
/// </summary
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

    [Column(name: "quest_id")]
    [Required(ErrorMessage = "Поле QuestId модели RoomBooking является обязательным")]
    public long QuestId { get; set; }

    [Column(name: "room_id")]
    [Required(ErrorMessage = "Поле RoomId модели RoomBooking является обязательным")]
    public long RoomId { get; set; }

    [Column(name: "QuestId")]
    public virtual Guest Quest { get; set; } = null!;

    [Column(name: "RoomId")]
    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<RoomPayment> RoomPayments { get; set; } = new List<RoomPayment>();
}
