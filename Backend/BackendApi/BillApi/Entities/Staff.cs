using ServicesCommon;

namespace BillApi.Entities
{
    public class Staff : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
