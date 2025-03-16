using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Comfort
{
    public int Id { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
