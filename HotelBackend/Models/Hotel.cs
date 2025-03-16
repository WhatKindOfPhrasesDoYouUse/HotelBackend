using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Hotel
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string? Description { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? YearOfConstruction { get; set; }

    public double Rating { get; set; }

    public virtual ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
