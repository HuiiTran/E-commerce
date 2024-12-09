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
    }
}
