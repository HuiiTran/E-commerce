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
        public required string ProductOperatingSystem { get; set; }
        public required string ProductConnectivity { get; set; }
        public required string ProductBatteryCapacity { get; set; }
        public required string ProductNetworkType { get; set; }
        public required string ProductRam {  get; set; }
        public required string ProductStorage {  get; set; }
        public required string ProductResolution { get; set; }
        public required string ProductRefeshRate { get; set; }
        public required string ProductSpecialFeature { get; set; }


        /// <summary>
        /// More information
        /// </summary>
        
        // public List<string>? ProductReivews {  get; set; }

        public bool isDeleted { get; set; } = false;
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
