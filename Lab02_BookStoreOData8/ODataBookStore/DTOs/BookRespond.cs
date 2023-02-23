namespace ODataBookStore.DTOs
{
    public class BookRespond
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
    }
}
