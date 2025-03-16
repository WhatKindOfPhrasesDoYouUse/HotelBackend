using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class AmenityBooking
{
    public int Id { get; set; }

    public DateOnly OrderDate { get; set; }

    public TimeOnly OrderTime { get; set; }

    public DateOnly ReadyDate { get; set; }

    public TimeOnly ReadyTime { get; set; }

    public string CompletionStatus { get; set; } = null!;

    public int Quantity { get; set; }

    public int AmenityId { get; set; }

    public int GuestId { get; set; }

    public virtual Amenity Amenity { get; set; } = null!;

    public virtual ICollection<AmenityPayment> AmenityPayments { get; set; } = new List<AmenityPayment>();

    public virtual Guest Guest { get; set; } = null!;
}
