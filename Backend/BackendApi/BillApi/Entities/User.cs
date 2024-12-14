using ServicesCommon;

namespace BillApi.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
