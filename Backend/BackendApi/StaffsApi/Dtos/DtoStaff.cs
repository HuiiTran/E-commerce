namespace StaffsApi.Dtos
{
    public record StaffDto(
        Guid Id,
        string UserName,
        string Password,
        string? Email,
        string? Name,
        string? Phone,
        string? Image,
        string? Addres,
        string Role,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateStaffDto(
        string UserName,
        string Password,
        string? Email,
        string? Name,
        string? Phone,
        IFormFile? Image
        );
    public record UpdateStaffDto(
        string UserName,
        string Password,
        string? Email,
        string? Name,
        string? Phone,
        IFormFile? Image,
        bool isDeleted
        );
}
