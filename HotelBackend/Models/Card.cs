using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Card
{
    public int Id { get; set; }

    public string CardNumber { get; set; } = null!;

    public string CardDate { get; set; } = null!;

    public int BankId { get; set; }

    public virtual Bank Bank { get; set; } = null!;

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
