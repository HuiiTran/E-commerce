using CartApi.Entities;

namespace CartsApi.Dto
{
    public record CartDto(
        Guid Id,
        Guid? UserId,
        List<ProductInCart> ListProductInCart,
        bool isDeleted,
        DateTimeOffset CreatedDate,
        DateTimeOffset UpdatedDate
        );
    public record CreateCartDto(
        List<ProductInCart> ListProductInCart
        );
    public record UpdateCartDto(
        List<ProductInCart>? ListProductInCart,
        bool isDeleted
        );
}
