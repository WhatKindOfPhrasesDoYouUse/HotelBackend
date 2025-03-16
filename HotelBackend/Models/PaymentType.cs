using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class PaymentType
{
    public int Id { get; set; }

    public virtual ICollection<AmenityPayment> AmenityPayments { get; set; } = new List<AmenityPayment>();

    public virtual ICollection<RoomPayment> RoomPayments { get; set; } = new List<RoomPayment>();
}
