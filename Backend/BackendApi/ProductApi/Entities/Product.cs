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
        public int ProductSoldQuantity { get; set; } = 0;
        public List<string>? ProductImages { get; set; }
        public required string ProductBrand { get; set; }
        public required string ProductType { get; set; }
        public string? ProductOperatingSystem { get; set; }
        public string? ProductConnectivity { get; set; }
        public string? ProductBatteryCapacity { get; set; }
        public string? ProductNetworkType { get; set; }
        public string? ProductRam {  get; set; }
        public string? ProductResolution { get; set; }
        public string? ProductRefeshRate { get; set; }
        public string? ProductSpecialFeature { get; set; }
        // public List<string>? ProductReivews {  get; set; }

        public bool isDeleted { get; set; } = false;
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
