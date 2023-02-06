using System;
using System.Collections.Generic;

namespace POS.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte Status { get; set; } = 1;

    public DateTime DateRegister { get; set; } = DateTime.Now;

    public int RolId { get; set; } = 1;

    public string Password { get; set; } = null!;

    public virtual Rol Rol { get; set; } = null!;
}
