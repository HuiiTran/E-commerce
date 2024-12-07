namespace ProductApi.Dtos
{
    public record ProductBatteryCapacityDto(
        Guid Id,
        string ProductBatteryCapacity,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
    );
    public record CreateProductBatteryCapacityDto(
        string ProductBatteryCapacity
    );
    public record UpdateProductBatteryCapacityDto(
        string ProductBatteryCapacity,
        bool isDeleted
    );
}
