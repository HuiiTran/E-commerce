namespace ProductApi.Dtos
{
    public record ProductRamDto(
        Guid Id,
        string ProductRamName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateProductRamDto(
        string ProductRamName
        );
    public record UpdateProductRamDto(
        string ProductRamName,
        bool isDeleted
        );
}
