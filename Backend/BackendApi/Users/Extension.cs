using UsersApi.Dtos;
using UsersApi.Entities;

namespace UsersApi
{
    public static class Extension
    {
        public static UserDto AsDto (this User user)
        {
            return new UserDto(
                user.Id,
                user.UserName,
                user.Password,
                user.Email,
                user.IsEmailConfirmed,
                user.ConfirmedCode,
                user.FullName,
                user.PhoneNumber,
                user.Address,
                user.BoughtProducts,
                user.isDeleted,
                user.CreatedDate,
                user.LatestUpdatedDate,
                user.Role
                );
        }
    }
}
