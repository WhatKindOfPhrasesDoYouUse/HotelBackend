using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class RoomBooking
{
    public int Id { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly CheckOutDate { get; set; }

    public TimeOnly CheckInTime { get; set; }

    public TimeOnly CheckOutTime { get; set; }

    public int QuestId { get; set; }

    public int RoomId { get; set; }

    public virtual Guest Quest { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;

    public virtual ICollection<RoomPayment> RoomPayments { get; set; } = new List<RoomPayment>();
}
