using ServicesCommon;

namespace CartApi.Entities
{
    public class CartProduct : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string? ProductImage { get; set; }
    }
}
