using ProductApi.Dtos;
using ProductApi.Entities;
using System.Runtime.CompilerServices;

namespace ProductApi
{
    public static class Extension
    {
        public static ProductDto AsDto(this Product product)
        {
            return new ProductDto(
                product.Id,
                product.ProductName,
                product.ProductDescription,
                product.ProductPrice,
                product.ProductQuantity,
                product.ProductSoldQuantity,
                product.ProductImages,
                product.ProductBrand,
                product.ProductType,
                product.ProductOperatingSystem,
                product.ProductConnectivity,
                product.ProductBatteryCapacity,
                product.ProductNetworkType,
                product.ProductRam,
                product.ProductStorage,
                product.ProductResolution,
                product.ProductRefeshRate,
                product.ProductSpecialFeature,
                product.isDeleted,
                product.CreatedDate,
                product.LatestUpdatedDate
                );
        }

        public static ProductBatteryCapacityDto ProductBatteryCapacityAsDto(this ProductBatteryCapacity productBatteryCapacity)
        {
            return new ProductBatteryCapacityDto(
                productBatteryCapacity.Id,
                productBatteryCapacity.ProductBatteryCapacityName,
                productBatteryCapacity.isDeleted,
                productBatteryCapacity.CreatedDate,
                productBatteryCapacity.LatestUpdatedDate
                );
        }

        public static ProductBrandDto ProductBrandAsDto(this ProductBrand productBrand)
        {
            return new ProductBrandDto(
                productBrand.Id,
                productBrand.BrandName,
                productBrand.isDeleted,
                productBrand.CreatedDate,
                productBrand.LatestUpdatedDate
                );
        }
    }
}
