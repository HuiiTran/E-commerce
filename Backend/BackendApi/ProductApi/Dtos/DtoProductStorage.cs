namespace ProductApi.Dtos
{
    public record ProductStorageDto(
        Guid Id,
        string ProductStorageName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateProductStorageDto(
        string ProductStorageName
        );
    public record UpdateProductStorageDto(
        string ProductStorageName,
        bool isDeleted
        );
}
