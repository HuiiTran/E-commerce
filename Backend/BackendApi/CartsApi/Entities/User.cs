using ServicesCommon;

namespace CartsApi.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        public string userName { get; set; }
    }
}
