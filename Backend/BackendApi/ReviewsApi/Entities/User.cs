using ServicesCommon;

namespace ReviewsApi.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public List<Guid>? BoughtProducts { get; set; } = new List<Guid>();
    }
}
