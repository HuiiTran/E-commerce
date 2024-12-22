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
        string Status,
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
    public record CreateBillDto(
        List<ProductInBill> ListProductInBill,
        string PhoneNumber,
        string Address
        );

    public record UpdateBillDto(
        string Status
        );
    

}
