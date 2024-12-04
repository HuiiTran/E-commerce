using CartApi.Entities;

namespace CartsApi.Dto
{
    public record CartDto(
        Guid Id,
        Guid? UserId,
        List<ProductInCart>? ListProductInCart,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset UpdatedDate
        );
    public record CreateDto(
        Guid? UserId,
        List<ProductInCart>? ListProductInCart
        );
    public record UpdateDto(
        List<ProductInCart>? ListProductInCart,
        bool isDeleted
        );
}
