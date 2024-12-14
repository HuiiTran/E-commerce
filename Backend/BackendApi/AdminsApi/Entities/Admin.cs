using ServicesCommon;

namespace AdminsApi.Entities
{
    public class Admin : IEntity
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role = "Admin";


        public bool isDeleted { get; set; } = false;
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }
    }
}
