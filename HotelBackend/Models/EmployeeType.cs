using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class EmployeeType
{
    public int Id { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
