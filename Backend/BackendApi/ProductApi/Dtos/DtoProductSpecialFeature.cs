namespace ProductApi.Dtos
{
    public record ProductSpecialFeatureDto(
        Guid Id,
        string ProductSpecialFeatureName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateProductSpecialFeatureDto(
        string ProductSpecialFeatureName
        );
    public record UpdateProductSpecialFeatureDto(
        string ProductSpecialFeatureName,
        bool isDeleted
        );
}
