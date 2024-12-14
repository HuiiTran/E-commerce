namespace UserContract
{
    public record UserCreate(
        Guid Id,
        string FullName,
        string UserName,
        string Password,
        string Role
        );
    public record UserUpdate(
        Guid Id,
        string FullName,
        string UserName,
        string Password
        );
    public record UserDelete(
        Guid Id
        );
}
