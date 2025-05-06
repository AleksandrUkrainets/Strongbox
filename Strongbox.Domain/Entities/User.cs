namespace Strongbox.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public PersonRole Role { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public ICollection<AccessRequest> AccessRequests { get; set; } = [];
        public ICollection<Decision> Decisions { get; set; } = [];
    }
}