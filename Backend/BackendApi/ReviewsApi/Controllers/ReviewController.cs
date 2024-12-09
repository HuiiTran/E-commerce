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
        private readonly IRepository<User> _userRepository;

        public ReviewController(IRepository<Review> reviewRepository, IRepository<User> userRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;   
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
                .Where (review => review.ProductId == id && review.isDeleted == false)
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
            var existingReview = (await _reviewRepository.GetAllAsync())
                .Where(eR => eR.OwnerId == createReviewDto.OwnerId && eR.ProductId == createReviewDto.ProductId)
                .FirstOrDefault();
            if (existingReview != null)
            {
                return BadRequest("You had reviewed this product");
            }
            var user = await _userRepository.GetAsync(createReviewDto.OwnerId);
            if ( user.BoughtProducts == null)
            {
                return BadRequest("You had not buy any product");
            }
            foreach (var product in user.BoughtProducts)
            {
                if(createReviewDto.ProductId != product)
                {
                    return BadRequest("You had not buy this product");
                }
            }

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
            }
            review.ReviewImages = tempImage;



            await _reviewRepository.CreateAsync(review);
            return Ok(review);
        }

        [HttpPut("{reviewId}")]
        public async Task<IActionResult> PutAsync(Guid reviewId, 
                                                   [FromQuery] Guid userId, 
                                                   [FromForm]UpdateReviewDto updateReviewDto)
            {
            var existingReview = (await _reviewRepository.GetAllAsync())
                .Where(r => r.Id == reviewId && r.OwnerId == userId)
                .FirstOrDefault();
            var tempListImages = existingReview.ReviewImages;
            var newListImages = new List<string>();
            if (existingReview != null)
            {
                existingReview.ReviewText = updateReviewDto.ReviewText;
                existingReview.Rated = updateReviewDto.Rated;
                existingReview.isDeleted = updateReviewDto.isDeleted;
                if (updateReviewDto != null)
                {
                    foreach(var image in updateReviewDto.ReviewImages)
                    {
                        if(image != null)
                        {
                            MemoryStream memoryStream = new MemoryStream();
                            image.OpenReadStream().CopyTo(memoryStream);
                            newListImages.Add(Convert.ToBase64String(memoryStream.ToArray()));
                        }
                    }
                    existingReview.ReviewImages = newListImages;
                }
                else
                {
                    existingReview.ReviewImages = tempListImages;
                }
                await _reviewRepository.UpdateAsync(existingReview);
                return Ok(existingReview);
            }
            return NotFound();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid reviewId, Guid userId)
        {
            var review = (await _reviewRepository.GetAllAsync())
                .Where(r => r.Id == reviewId && r.OwnerId == userId)
                .FirstOrDefault();
            if (review != null)
            {
                review.isDeleted = true;
                await _reviewRepository.UpdateAsync(review);
            }
            return NotFound();
        }
    }
}
