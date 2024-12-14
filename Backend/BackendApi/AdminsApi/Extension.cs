using AdminsApi.Dtos;
using AdminsApi.Entities;

namespace AdminsApi
{
    public static class Extension
    {
        public static AdminDto AsDto (this Admin admin)
        {
            return new AdminDto(
                admin.Id,
                admin.UserName,
                admin.Password,
                admin.Email,
                admin.Name,
                admin.PhoneNumber,
                admin.Role,
                admin.isDeleted,
                admin.CreatedDate,
                admin.LatestUpdatedDate
                );
        }
    }
}
