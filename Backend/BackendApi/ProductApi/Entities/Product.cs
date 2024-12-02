using ServicesCommon;

namespace ProductApi.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductSoldQuantity { get; set; }
        public List<string>? ProductImages { get; set; }
        public required string ProductBrand { get; set; }
        public required string ProductType { get; set; }
        // public List<string>? ProductReivews {  get; set; }

        public bool isDeleted { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? LatestUpdatedDate { get; set; }
    }
}
