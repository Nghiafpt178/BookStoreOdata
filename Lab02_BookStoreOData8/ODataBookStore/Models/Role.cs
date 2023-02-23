using System;
using System.Collections.Generic;

namespace ODataBookStore.Models;

public partial class Role
{
    public bool RoleId { get; set; }

    public string? RoleDesc { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
