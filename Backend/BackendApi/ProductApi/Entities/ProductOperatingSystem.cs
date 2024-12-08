using ServicesCommon;

namespace ProductApi.Entities
{
    public class ProductOperatingSystem : IEntity
    {
        public Guid Id { get; set; }

        public required string ProductOperatingSystemName { get; set; }



        public bool isDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
