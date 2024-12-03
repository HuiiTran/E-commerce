using ServicesCommon;

namespace ReviewsApi.Entities
{
    public class Review : IEntity
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string? ReviewText {  get; set; }
        public List<string>? ReivewImages { get; set; }

        public int Rated {  get; set; }
    }
}
