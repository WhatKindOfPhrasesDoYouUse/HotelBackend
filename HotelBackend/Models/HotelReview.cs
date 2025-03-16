using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class HotelReview
{
    public int Id { get; set; }

    public string? Comment { get; set; }

    public DateOnly PublicationDate { get; set; }

    public TimeOnly PublicationTime { get; set; }

    public int Rating { get; set; }

    public int GuestId { get; set; }

    public int HotelId { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual Hotel Hotel { get; set; } = null!;
}
