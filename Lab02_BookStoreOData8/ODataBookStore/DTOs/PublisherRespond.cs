namespace ODataBookStore.DTOs
{
    public class PublisherRespond
    {
        public string PubId { get; set; } = null!;

        public string PublisherName { get; set; } = null!;

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }
    }
}
