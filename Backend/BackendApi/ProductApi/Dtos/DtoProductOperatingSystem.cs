namespace ProductApi.Dtos
{
    public record ProductOperatingSystemDto(
        Guid Id,
        string ProductOperatingSystemName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateProductOperatingSystemDto(
        string ProductOperatingSystemName
        );
    public record UpdateProductOperatingSystemDto(
        string ProductOperatingSystemName,
        bool isDeleted
        );
}
