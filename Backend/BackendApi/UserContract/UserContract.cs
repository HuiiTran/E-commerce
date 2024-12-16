namespace UserContract
{
    public record UserCreate(
        Guid Id,
        string FullName,
        string UserName,
        string Password,
        List<Guid> BoughtProducts,
        string Role
        );
    public record UserUpdate(
        Guid Id,
        string FullName,
        string UserName,
        string Password,
        List<Guid> BoughtProducts
        );
    public record UserDelete(
        Guid Id
        );
}
