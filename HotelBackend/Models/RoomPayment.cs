using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class RoomPayment
{
    public int Id { get; set; }

    public DateOnly PaymentDate { get; set; }

    public TimeOnly PaymentTime { get; set; }

    public decimal TotalAmount { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public int PaymentTypeId { get; set; }

    public int RoomBookingId { get; set; }

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual RoomBooking RoomBooking { get; set; } = null!;
}
