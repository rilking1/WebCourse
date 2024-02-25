using System;
using System.Collections.Generic;

namespace WebApplication4.Data;

public partial class Student : Entity
{
    //public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
