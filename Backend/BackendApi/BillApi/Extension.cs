using BillApi.Dto;
using BillApi.Entities;

namespace BillApi
{
    public static class Extension
    {
        public static BillDto BillAsDto(
            this Bill bill, 
            List<Product> listProduct,
            string OwnerName)
        {
            return new BillDto(
                bill.Id,
                bill.OwnerCreatedId,
                OwnerName,
                listProduct,
                bill.TotalPrice,
                bill.PhoneNumber,
                bill.Address,
                bill.isDeleted,
                bill.CreatedDate,
                bill.UpdatedDate
                );
        }
        public static ListBillDto ListBillAsDto(this Bill bill, string OwnerName)
        {
            return new ListBillDto(
                bill.Id,
                bill.OwnerCreatedId,
                OwnerName,
                bill.TotalPrice,
                bill.CreatedDate
                );
        }
    }
}
