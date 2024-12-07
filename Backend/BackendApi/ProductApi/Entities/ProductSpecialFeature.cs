using ServicesCommon;

namespace ProductApi.Entities
{
    public class ProductSpecialFeature : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductSpecialFeatureName { get; set; }



        public bool isDeleted { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? LatestUpdatedDate { get; set; }
    }
}
