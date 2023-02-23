using System;
using System.Collections.Generic;

namespace ODataBookStore.Models;

public partial class Book
{
    public string BookId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Type { get; set; }

    public string PubId { get; set; } = null!;

    public decimal? Price { get; set; }

    public decimal? Advance { get; set; }

    public decimal? Royalty { get; set; }

    public int? YtdSales { get; set; }

    public string? Notes { get; set; }

    public DateTime? PublishedDate { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; } = new List<BookAuthor>();

    public virtual Publisher Pub { get; set; } = null!;
}
