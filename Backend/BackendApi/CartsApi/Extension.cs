using CartApi.Entities;
using CartsApi.Dto;

namespace CartsApi
{
    public static class Extension
    {
        public static CartDto AsDto(this Cart cart)
        {
            return new CartDto(
                cart.Id,
                cart.UserId,
                cart.ListProductInCart,
                cart.isDeleted,
                cart.CreatedDate,
                cart.UpdatedDate
                );
        }

        public static SingleCartDto SingleCartDtoAsDto(this Cart cart, List<Product> cartProducts)
        {
            return new SingleCartDto(
                cart.Id,
                cart.UserId,
                cartProducts,
                cart.isDeleted,
                cart.CreatedDate,
                cart.UpdatedDate
                );
        }

        public static CartInformationDto CartInformationAsDto(this Cart cart, List<Product> cartProducts,  string userName, decimal totalPrice)
        {
            return new CartInformationDto(
                cart.Id,
                cart.UserId,
                cartProducts,
                cart.isDeleted,
                cart.CreatedDate,
                cart.UpdatedDate,
                userName,
                totalPrice
                );
        }
    }
}
