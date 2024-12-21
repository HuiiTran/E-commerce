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
        List<Guid> BoughtProducts,
        string Role
        );
    public record UserDelete(
        Guid Id
        );
    public record UserUpdatePassword(
        string newPassword
        );
    public record UserUpdateBoughtProduct(
        List<Guid> listBoughtProduct
        );
}
