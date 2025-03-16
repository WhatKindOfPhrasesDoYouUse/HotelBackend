using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Bank
{
    public int Id { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
