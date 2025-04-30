namespace Strongbox.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public PersonRole Role { get; set; }
        public ICollection<AccessRequest> AccessRequests { get; set; } = [];
    }
}