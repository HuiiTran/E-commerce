using ServicesCommon;

namespace ProductApi.Entities
{
    public class ProductStorage : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductStorageName { get; set; }

        public bool isDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
