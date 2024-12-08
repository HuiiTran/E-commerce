namespace ProductApi.Dtos
{
    public record ProductBrandDto(
        Guid Id,
        string ProductBrandName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LastestUpdatedDate
        );
    public record CreateProductBrandDto(
        string ProductBrandName
        );
    public record UpdateProductBrandDto(
        string ProductBrandName,
        bool isDeleted
        );
}
