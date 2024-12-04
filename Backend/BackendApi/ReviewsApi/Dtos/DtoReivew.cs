namespace ReviewsApi.Dto
{
    public record ReviewDto(
        Guid Id,
        Guid ProductId,
        string? ReviewText,
        List<string>? ReviewImages,
        int Rated,
        bool isDeleted
        );
    public record CreateReviewDto(
        Guid ProductId,
        string? ReviewText,
        List<string>? ReviewImages,
        int Rated
        );
    public record UpdateReviewDto(
        string? ReviewText,
        List<string>? ReviewImages,
        int Rated,
        bool isDeleted
        );
}
