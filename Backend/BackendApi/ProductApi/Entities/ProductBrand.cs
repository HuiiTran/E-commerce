using ServicesCommon;

namespace ProductApi.Entities
{
    public class ProductBrand : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductBrandName {  get; set; }



        public bool isDeleted { get; set; } = false;
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
