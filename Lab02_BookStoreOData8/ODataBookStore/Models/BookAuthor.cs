using System;
using System.Collections.Generic;

namespace ODataBookStore.Models;

public partial class BookAuthor
{
    public string AuthorId { get; set; } = null!;

    public string BookId { get; set; } = null!;

    public byte? AuthorOrder { get; set; }

    public byte? RoyalityPercentage { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;
}
