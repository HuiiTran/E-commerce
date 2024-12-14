namespace StaffContract
{
    public record StaffCreate(
        Guid Id,
        string UserName,
        string Password,
        string Role
        );
    public record StaffUpdate(
        Guid Id,
        string UserName,
        string Password,
        string Role
        );
    public record StaffDelet(
        Guid Id
        );
}
