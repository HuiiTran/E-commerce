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
                product.isDeleted,
                product.CreatedDate,
                product.LatestUpdatedDate
                );
        }
    }
}
