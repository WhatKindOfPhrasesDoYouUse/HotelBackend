using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Amenity
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int UnitPrice { get; set; }

    public int RoomId { get; set; }

    public virtual ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();

    public virtual ICollection<AmenityReview> AmenityReviews { get; set; } = new List<AmenityReview>();

    public virtual Room Room { get; set; } = null!;
}
