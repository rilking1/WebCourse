using System;
using System.Collections.Generic;

namespace WebApplication4.Data;

public partial class TeachersSpecialization : Entity
{
    //public int Id { get; set; }

    public int TeachersId { get; set; }

    public int SpecializationId { get; set; }

    public virtual Specialization? Specialization { get; set; }

    public virtual Teacher? Teachers { get; set; }
}
