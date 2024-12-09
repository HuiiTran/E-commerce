using ServicesCommon;

namespace CartApi.Entities
{
    public class Cart : IEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public List<ProductInCart> ListProductInCart { get; set; } = [];


        //
        public bool isDeleted { get; set; } = false;

        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
    }
}
