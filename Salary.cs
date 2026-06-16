using System;
using System.Collections.Generic;

namespace UniversityManagementSystem.Models;

public partial class Salary
{
    public int SId { get; set; }

    public decimal Base { get; set; }

    public decimal Bonus { get; set; }

    public decimal? Total { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
