using ReviewsApi.Dto;
using ReviewsApi.Entities;

namespace ReviewsApi
{
    public static class Extension
    {
        public static ReviewDto AsDto(this Review review)
        {
            return new ReviewDto(
                review.Id,
                review.ProductId,
                review.OwnerId,
                review.ReviewText,
                review.ReviewImages,
                review.Rated,
                review.isDeleted
                );
        }
    }
}
