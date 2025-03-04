﻿using ProductApi.Entities;
using RabbitMQ.Client;

namespace ProductApi.Dtos
{
    public record ImportProductBillsDto(
        Guid id,
        Guid personId,
        List<Product> Products,
        string PersonName,
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
        Guid personId,
        List<ImportProduct> ImportProducts
        );
}
