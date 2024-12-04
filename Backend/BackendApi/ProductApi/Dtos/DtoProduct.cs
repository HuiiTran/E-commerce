using System.Runtime.CompilerServices;

namespace ProductApi.Dtos
{
    public record ProductDto(
        Guid Id, 
        string ProductName, 
        string ProductDescription,
        decimal ProductPrice,
        int ProductQuantity,
        int ProductSoldQuantity,
        List<string>? ProductImages,
        string ProductBrand,
        string ProductType,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
    );

    public record CreateProductDto(
        string ProductName,
        string ProductDescription,
        decimal ProductPrice,
        int ProductQuantity,
        List<string> ProductImages,
        string ProductBrand,
        string ProductType
        );
    public record UpdateProductDto(
        string ProductName,
        string ProductDescription,
        decimal ProductPrice,
        int ProductQuantity,
        List<string> ProductImages,
        string ProductBrand,
        string ProductType,
        bool isDeleted
        );

}
