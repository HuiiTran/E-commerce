namespace ProductApi.Dtos
{
    public record ProductConnectivityDto(
        Guid Id,
        string ProductConnectivityName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LatestUpdatedDate
        );
    public record CreateProductConnectivityDto(
        string ProductConnectivityName
        );
    public record UpdateProductConnectivityDto(
        string ProductConnectivityName,
        bool isDeleted
        );
}
