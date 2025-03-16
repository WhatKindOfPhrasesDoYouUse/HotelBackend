using System;
using System.Collections.Generic;

namespace HotelBackend.Models;

public partial class Employee
{
    public int Id { get; set; }

    public DateOnly HireDate { get; set; }

    public decimal BaseSalary { get; set; }

    public int EmployeeTypeId { get; set; }

    public int? WorkScheduleId { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual EmployeeType EmployeeType { get; set; } = null!;

    public virtual WorkSchedule? WorkSchedule { get; set; }
}
