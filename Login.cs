using System;
using System.Collections.Generic;

namespace UniversityManagementSystem.Models;

public partial class Login
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
