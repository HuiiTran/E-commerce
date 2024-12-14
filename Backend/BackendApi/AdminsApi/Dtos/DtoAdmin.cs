using RabbitMQ.Client;

namespace AdminsApi.Dtos
{
    public record AdminDto(
        Guid Id,
        string UserName,
        string Password,
        string? Email,
        string? Name,
        string? PhoneNumber,
        string Role,
        bool isDeleted,
        DateTimeOffset Createddate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateAdminDto(
        string UserName,
        string Password,
        string? Email,
        string? Name,
        string? PhoneNumber
        );
    public record UpdateAdminDto(
        string UserName,
        string Password,
        string? Email,
        string? Name,
        string? PhoneNumber,
        bool isDeleted
        );
}
