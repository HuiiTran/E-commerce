using ServicesCommon;

namespace ReviewsApi.Entities
{
    public class Review : IEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid OwnerId { get; set; }

        public string? ReviewText {  get; set; }
        public List<string>? ReviewImages { get; set; }

        public int Rated {  get; set; }

        public bool isDeleted { get; set; } = false;
    }
}
