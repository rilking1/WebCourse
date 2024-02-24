using System;
using System.Collections.Generic;

namespace WebApplication4.Data;

public partial class Teacher
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public string FullName => FirstName + ' ' + LastName;

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<TeachersSpecialization> TeachersSpecializations { get; set; } = new List<TeachersSpecialization>();
}
