namespace AdminContract
{
    public record AdminCreate(
        Guid Id,
        string UserName,
        string Password,
        string Role
        );
    public record AdminUpdate(
        Guid Id,
        string UserName,
        string Password,
        string Role
        );
    public record AdminDelet(
        Guid Id
        );
}
