using ProductApi.Entities;
using RabbitMQ.Client;

namespace ProductApi.Dtos
{
    public record ImportProductBillsDto(
        Guid id,
        Guid personId,
        List<Product> Products,
        string personName,
        Decimal total,
        bool isDeleted,
        DateTimeOffset CreatedDate
        );
    public record AllImportProductBillDto(
        Guid id,
        Decimal total,
        DateTimeOffset CreatedDate
        );
    public record CreateImportProductBillsDto(
        List<ImportProduct> ImportProducts
        );
}
