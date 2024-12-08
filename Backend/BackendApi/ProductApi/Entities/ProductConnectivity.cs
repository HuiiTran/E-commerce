using ServicesCommon;

namespace ProductApi.Entities
{
    public class ProductConnectivity : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductConnectivityName { get; set; }

        public bool isDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
