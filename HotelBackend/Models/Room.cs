using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Room
{
    public int Id { get; set; }

    public int RoomNumber { get; set; }

    public string? Description { get; set; }

    public int Capacity { get; set; }

    public int UnitPrice { get; set; }

    public int HotelId { get; set; }

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

    public virtual ICollection<Comfort> Comforts { get; set; } = new List<Comfort>();
}
