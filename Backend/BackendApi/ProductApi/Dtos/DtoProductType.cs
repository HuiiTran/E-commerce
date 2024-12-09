namespace ProductApi.Dtos
{
    public record ProductTypeDto(
        Guid Id,
        string TypeName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateProductTypeDto(
        string TypeName
        );
    public record UpdateProductTypeDto(
        string TypeName,
        bool isDeleted
        );
}
