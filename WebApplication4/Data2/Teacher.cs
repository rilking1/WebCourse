//using System;
//using System.Collections.Generic;
//using WebApplication4.Data;

//namespace WebApplication4.Data2;

//public partial class Teacher
//{
//    public int Id { get; set; }

//    public string Username { get; set; } = null!;

//    public string Password { get; set; } = null!;

//    public string Email { get; set; } = null!;

//    public string? FirstName { get; set; }

//    public string? LastName { get; set; }

//    public string? PhoneNumber { get; set; }

//    public int? PhotoUrlId { get; set; }

//    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

//    public virtual Photo? PhotoUrl { get; set; }
//}
