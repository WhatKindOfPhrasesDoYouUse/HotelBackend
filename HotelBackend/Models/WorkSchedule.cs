using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class WorkSchedule
{
    public int Id { get; set; }

    public DateOnly WorkDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
