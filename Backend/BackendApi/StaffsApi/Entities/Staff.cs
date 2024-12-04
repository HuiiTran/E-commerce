using ServicesCommon;

namespace StaffsApi.Entities
{
    public class Staff : IEntity
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Image { get; set; }


        public bool isDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }


        //public List<decimal>? Salary { get; set; }
    }
}
