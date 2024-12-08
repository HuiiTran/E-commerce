namespace ProductApi.Dtos
{
    public record ProductNetworkTypeDto(
        Guid Id,
        string ProductNetworkTypeName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateProductNetworkTypeDto(
        string ProductNetworkTypeName
        );
    public record UpdateProductNetworkTypeDto(
        string ProductNetworkTypeName,
        bool isDeleted
        );
}
