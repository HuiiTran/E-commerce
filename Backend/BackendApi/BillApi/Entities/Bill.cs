using ServicesCommon;

namespace BillApi.Entities
{
    public class Bill : IEntity
    {
        public Guid Id { get; set; }

        public Guid OwnerCreatedId { get; set; }

        public List<ProductInBill> ListProductInBill { get; set; } = new List<ProductInBill>();

        public decimal TotalPrice { get; set; } = 0;

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Status { get; set; } 
        public bool isDeleted { get; set; } = false;

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
