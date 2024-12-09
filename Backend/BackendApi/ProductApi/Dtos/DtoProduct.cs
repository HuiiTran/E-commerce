using ProductApi.Entities;
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
        string ProductOperatingSystem,
        string ProductConnectivity,
        string ProductBatteryCapacity,
        string ProductNetworkType,
        string ProductRam,
        string ProductStorage,
        string ProductResolution,
        string ProductRefeshRate,
        string ProductSpecialFeature,
        int DiscountPercentage,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
    );

    public record CreateProductDto(
        string ProductName,
        string ProductDescription,
        decimal ProductPrice,
        int ProductQuantity,
        List<IFormFile> ProductImages,
        string ProductBrand,
        string ProductType,
        string ProductOperatingSystem,
        string ProductConnectivity,
        string ProductBatteryCapacity,
        string ProductNetworkType,
        string ProductRam,
        string ProductStorage,
        string ProductResolution,
        string ProductRefeshRate,
        string ProductSpecialFeature
        );
    public record UpdateProductDto(
        string ProductName,
        string ProductDescription,
        decimal ProductPrice,
        int ProductQuantity,
        List<IFormFile> ProductImages,
        string ProductBrand,
        string ProductType,
        string ProductOperatingSystem,
        string ProductConnectivity,
        string ProductBatteryCapacity,
        string ProductNetworkType,
        string ProductRam,
        string ProductStorage,
        string ProductResolution,
        string ProductRefeshRate,
        string ProductSpecialFeature,
        bool isDeleted,
        int DiscountPercentage
        );

}
