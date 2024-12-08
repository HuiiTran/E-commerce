namespace ProductApi.Dtos
{
    public record ProductResolutionDto(
        Guid Id,
        string ProductResolutionName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateProductResolutionDto(
        string ProductResolutionName
        );
    public record UpdateProductResolutionDto(
        string ProductResolutionName,
        bool isDeleted
        );
}
