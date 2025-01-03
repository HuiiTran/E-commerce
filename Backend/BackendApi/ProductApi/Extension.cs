﻿using MassTransit.SagaStateMachine;
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
                product.DiscountPercentage,
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
                productBrand.ProductBrandName,
                productBrand.isDeleted,
                productBrand.CreatedDate,
                productBrand.LatestUpdatedDate
                );
        }

        public static ProductConnectivityDto ProductConnectivityAsDto(this ProductConnectivity productConnectivity)
        {
            return new ProductConnectivityDto(
                productConnectivity.Id,
                productConnectivity.ProductConnectivityName,
                productConnectivity.isDeleted,
                productConnectivity.CreatedDate,
                productConnectivity.LatestUpdatedDate
                );
        }

        public static ProductNetworkTypeDto ProductNetworkTypeAsDto(this ProductNetworkType productNetworkType)
        {
            return new ProductNetworkTypeDto(
                productNetworkType.Id,
                productNetworkType.ProductNetworkTypeName,
                productNetworkType.isDeleted,
                productNetworkType.CreatedDate,
                productNetworkType.LatestUpdatedDate
                );
        }

        public static ProductOperatingSystemDto ProductOperatingSystemAsDto(this ProductOperatingSystem productOperatingSystem)
        {
            return new ProductOperatingSystemDto(
                productOperatingSystem.Id,
                productOperatingSystem.ProductOperatingSystemName,
                productOperatingSystem.isDeleted,
                productOperatingSystem.CreatedDate,
                productOperatingSystem.LatestUpdatedDate
                );
        }

        public static ProductRamDto ProductRamAsDto(this ProductRam productRam)
        {
            return new ProductRamDto(
                productRam.Id,
                productRam.ProductRamName,
                productRam.isDeleted,
                productRam.CreatedDate,
                productRam.LatestUpdatedDate
                );
        }

        public static ProductRefeshRateDto ProductRefeshRateAsDto(this ProductRefeshRate productRefeshRate)
        {
            return new ProductRefeshRateDto(
                productRefeshRate.Id,
                productRefeshRate.ProductRefeshRateName,
                productRefeshRate.isDeleted,
                productRefeshRate.CreatedDate,
                productRefeshRate.LatestUpdatedDate
                );
        }

        public static ProductResolutionDto productResolutionAsDto(this ProductResolution productResolution)
        {
            return new ProductResolutionDto(
                productResolution.Id,
                productResolution.ProductResolutionName,
                productResolution.isDeleted,
                productResolution.CreatedDate,
                productResolution.LatestUpdatedDate
                );
        }

        public static ProductSpecialFeatureDto ProductSpecialFeatureAsDto(this ProductSpecialFeature productSpecialFeature)
        {
            return new ProductSpecialFeatureDto(
                productSpecialFeature.Id,
                productSpecialFeature.ProductSpecialFeatureName,
                productSpecialFeature.isDeleted,
                productSpecialFeature.CreatedDate,
                productSpecialFeature.LatestUpdatedDate
                );
        }

        public static ProductStorageDto ProductStorageAsDto(this ProductStorage productStorage)
        {
            return new ProductStorageDto(
                productStorage.Id,
                productStorage.ProductStorageName,
                productStorage.isDeleted,
                productStorage.CreatedDate,
                productStorage.LatestUpdatedDate
                );
        }

        public static ProductTypeDto ProductTypeAsDto(this ProductType productType)
        {
            return new ProductTypeDto(
                productType.Id,
                productType.TypeName,
                productType.isDeleted,
                productType.CreatedDate,
                productType.LatestUpdatedDate
                );
        }

        public static ImportProductBillsDto importProductBillsAsDto(this ImportProductBill importProductBill, List<Product> productList, string personName )
        {
            return new ImportProductBillsDto(
                importProductBill.Id,
                importProductBill.PersonId,
                productList,
                personName,
                importProductBill.Total,
                importProductBill.IsDeleted,
                importProductBill.CreatedDate
                );
        }

        public static AllImportProductBillDto AllImportProductBillAsDto(this ImportProductBill importProductBill)
        {
            return new AllImportProductBillDto(
                importProductBill.Id,
                importProductBill.Total,
                importProductBill.CreatedDate
                );
        }
    }
}
