namespace ProductApi.Dtos
{
    public record ProductBatteryCapacityDto(
        Guid Id,
        string ProductBatteryCapacityName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
    );
    public record CreateProductBatteryCapacityDto(
        string ProductBatteryCapacityName
    );
    public record UpdateProductBatteryCapacityDto(
        string ProductBatteryCapacityName,
        bool isDeleted
    );
}
