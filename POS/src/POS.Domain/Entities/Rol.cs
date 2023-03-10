using System;
using System.Collections.Generic;

namespace POS.Domain.Entities;

public partial class Rol
{
    public int RolId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
