using ServicesCommon;

namespace ProductApi.Entities
{
    public class ProductResolution : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductResolutionName { get; set; }



        public bool isDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
