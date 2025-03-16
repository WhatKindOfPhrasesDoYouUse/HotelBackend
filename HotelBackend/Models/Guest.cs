using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Guest
{
    public int Id { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public string PassportSeriesHash { get; set; } = null!;

    public string PassportNumberHash { get; set; } = null!;

    public int? CardId { get; set; }

    public int ClientId { get; set; }

    public virtual ICollection<AmenityBooking> AmenityBookings { get; set; } = new List<AmenityBooking>();

    public virtual ICollection<AmenityReview> AmenityReviews { get; set; } = new List<AmenityReview>();

    public virtual Card? Card { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();
}
