namespace ReviewsApi.Dto
{
    public record ReviewDto(
        Guid Id,
        Guid ProductId,
        Guid OwnerId,
        string? ReviewText,
        List<string>? ReviewImages,
        int Rated,
        bool isDeleted
        );
    public record CreateReviewDto(
        Guid ProductId,
        Guid OwnerId,
        string? ReviewText,
        List<IFormFile>? ReviewImages,
        int Rated
        );
    public record UpdateReviewDto(
        string? ReviewText,
        List<IFormFile>? ReviewImages,
        int Rated,
        bool isDeleted
        );
}
