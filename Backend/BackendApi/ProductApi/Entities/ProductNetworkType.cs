using ServicesCommon;

namespace ProductApi.Entities
{
    public class ProductNetworkType : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductNetworkTypeName { get; set; }



        public bool isDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
