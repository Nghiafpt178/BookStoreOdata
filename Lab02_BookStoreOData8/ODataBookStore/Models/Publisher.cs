﻿using System;
using System.Collections.Generic;

namespace ODataBookStore.Models;

public partial class Publisher
{
    public string PubId { get; set; } = null!;

    public string PublisherName { get; set; } = null!;

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
