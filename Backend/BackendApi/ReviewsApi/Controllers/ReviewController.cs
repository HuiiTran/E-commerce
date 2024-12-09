using Microsoft.AspNetCore.Mvc;
using ReviewsApi.Dto;
using ReviewsApi.Entities;
using ServicesCommon;

namespace ReviewsApi.Controllers
{
    [ApiController]
    [Route("Review")]
    public class ReviewController : ControllerBase
    {
        private readonly IRepository<Review> _reviewRepository;

        public ReviewController(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllAsync()
        {
            var reviews = (await _reviewRepository.GetAllAsync())
                .Select(review => review.AsDto());
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetByIdAsync(Guid id)
        {
            var review = await _reviewRepository.GetAsync(id);

            if (review == null) return NotFound();

            return review.AsDto();
        }
        [HttpGet("reivewProductId={id}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllProductReview(Guid id)
        {
            var reviews = (await _reviewRepository.GetAllAsync())
                .Where(review => review.ProductId == id)
                .Select(review => review.AsDto());
            return Ok(reviews);
        }
        [HttpGet("averageReview={id}")]
        public async Task<ActionResult<float>> GetAverageReview(Guid id)
        {
            float averageReview = 0;
            float totalScore = 0;
            var reviews = (await _reviewRepository.GetAllAsync())
                .Where (review => review.ProductId == id)
                .Select (review => review.Rated);
/*            var reviewNumber = (await _reviewRepository.GetAllAsync())
                .Where(review => review.ProductId == id)
                .Count();*/
            foreach (var review in reviews)
            {
                totalScore += review;
            }
            averageReview = totalScore / reviews.Count();
            return Ok(averageReview); 
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> PostAsync([FromForm]CreateReviewDto createReviewDto)
        {
            var review = new Review
            {
                ProductId = createReviewDto.ProductId,
                OwnerId = createReviewDto.OwnerId,
                ReviewText = createReviewDto.ReviewText,
                Rated = createReviewDto.Rated,
                isDeleted = false
            };
            List<string> tempImage = new List<string>();
            if(createReviewDto.ReviewImages != null)
            {
                foreach (var image in createReviewDto.ReviewImages)
                {
                    if (image != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        image.OpenReadStream().CopyTo(memoryStream);
                        tempImage.Add(Convert.ToBase64String(memoryStream.ToArray()));
                    }
                }
                review.ReviewImages = tempImage;
            }
            review.ReviewImages = null;
            

            await _reviewRepository.CreateAsync(review);
            return Ok(review);
        }
    }
}
