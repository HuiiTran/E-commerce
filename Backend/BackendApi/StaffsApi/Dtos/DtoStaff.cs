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
        string? Image
        );
    public record UpdateStaffDto(
        string UserName,
        string Password,
        string? Email,
        string? Name,
        string? Phone,
        string? Image,
        bool isDeleted
        );
}
