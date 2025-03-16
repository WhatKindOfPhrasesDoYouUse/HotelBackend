using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class AmenityPayment
{
    public int Id { get; set; }

    public DateOnly PaymentDate { get; set; }

    public TimeOnly PaymentTime { get; set; }

    public int TotalCost { get; set; }

    public string? PaymentStatus { get; set; }

    public int PaymentTypeId { get; set; }

    public int AmenityBookingId { get; set; }

    public virtual AmenityBooking AmenityBooking { get; set; } = null!;

    public virtual PaymentType PaymentType { get; set; } = null!;
}
