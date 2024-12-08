namespace ProductApi.Dtos
{
    public record ProductRefeshRateDto(
        Guid Id,
        string ProductRefeshRateName,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset LastestUpdateDate
        );
    public record CreateProductRefeshRateDto(
        string ProductRefeshRateName
        );
    public record UpdateProductRefeshRateDto(
        string ProductRefeshRateName,
        bool isDeleted
        );
}
