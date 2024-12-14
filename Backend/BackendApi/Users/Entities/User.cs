using ServicesCommon;

namespace UsersApi.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool IsEmailConfirmed { get; set; } = false;
        public int? ConfirmedCode { get; set; }

        public string? FullName { get; set; }
        public List<string>? PhoneNumber { get; set; }
        public List<string>? Address { get; set; }
        public string Role = "User";

        //public Guid CartId { get; set; }


        public List<Guid>? BoughtProducts {  get; set; } = new List<Guid>();


        public bool isDeleted { get; set; } = false;
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LatestUpdatedDate { get; set; }

    }
}
