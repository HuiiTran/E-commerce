using ServicesCommon;

namespace ProductApi.Entities
{
    public class ProductType : IEntity
    {
        public Guid Id { get; set; }

        public required string TypeName { get; set; }

        
        
        

        /// <summary>
        /// Tracking
        /// </summary>
        public bool isDeleted { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? LatestUpdatedDate { get; set; }
    }
}
