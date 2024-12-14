using ServicesCommon;

namespace ProductApi.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
