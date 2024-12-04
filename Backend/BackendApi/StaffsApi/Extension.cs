using StaffsApi.Dtos;
using StaffsApi.Entities;

namespace StaffsApi
{
    public static class Extension
    {
        public static StaffDto AsDto (this Staff staff)
        {
            return new StaffDto(
                staff.Id,
                staff.UserName,
                staff.Password,
                staff.Email,
                staff.Name,
                staff.PhoneNumber,
                staff.Image,
                staff.isDeleted,
                staff.CreatedDate,
                staff.LatestUpdatedDate
                );
        }
    }
}
