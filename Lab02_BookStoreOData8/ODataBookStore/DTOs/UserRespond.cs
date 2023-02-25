namespace ODataBookStore.DTOs
{
	public class UserRespond
	{
        public string UserId { get; set; } = null!;

        public string EmailAddress { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Source { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public bool RoleId { get; set; }

        public string PubId { get; set; } = null!;

        public DateTime HireDate { get; set; }
    }
}
