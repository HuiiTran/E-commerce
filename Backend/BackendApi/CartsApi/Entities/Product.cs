using ServicesCommon;

namespace CartApi.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int DiscountPrecentage { get; set; }
        public int ProductQuantity { get; set; }
        public string? ProductImage { get; set; }
    }
}
