using BillApi.Entities;

namespace BillApi.Dto
{
    public record BillDto(
        Guid Id,
        Guid OwnerCreatedId,
        string OwnerName,
        List<Product> ListProduct,
        decimal TotalPrice,
        string PhoneNumber,
        string Address,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset UpdateDate
        );

    public record ListBillDto(
        Guid Id,
        Guid OwnerCreatedId,
        string OwnerName,
        decimal TotalPrice,
        DateTimeOffset CreatedDate
        );
    public record CreateCartDto(
        List<ProductInBill> ListProductInBill,
        string PhoneNumber,
        string Address
        );
    

}
