using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();
}
