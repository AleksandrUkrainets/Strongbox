namespace Strongbox.Domain.Entities
{
    public enum AccessType
    {
        Read,
        Edit
    }

    public enum PersonRole
    {
        User,
        Approver,
        Admin
    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
