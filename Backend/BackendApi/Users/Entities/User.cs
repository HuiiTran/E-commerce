using ServicesCommon;

namespace UsersApi.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? Email { get; set; }
        public bool IsEmailConfirmed { get; set; }

        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Address { get; set; }

        public List<Guid>? BoughtProducts {  get; set; }


        public bool isDeleted { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? LatestUpdatedDate { get; set; }

    }
}
