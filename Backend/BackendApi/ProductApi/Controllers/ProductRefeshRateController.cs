using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductRefeshRate")]
    public class ProductRefeshRateController : ControllerBase
    {
        private IRepository<ProductRefeshRate> _productRefeshRateRepository;

        public ProductRefeshRateController(IRepository<ProductRefeshRate> productRefeshRateRepository)
        {
            _productRefeshRateRepository = productRefeshRateRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductRefeshRateDto>>> GetAsync()
        {
            var productRefeshRates = (await _productRefeshRateRepository.GetAllAsync())
                .Select(productRefeshRate => productRefeshRate.ProductRefeshRateAsDto());
            return Ok(productRefeshRates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRefeshRateDto>> GetByIdAsync(Guid id)
        {
            var productRefeshRate = await _productRefeshRateRepository.GetAsync(id);
            if(productRefeshRate == null)
            {
                return NotFound();
            }
            return productRefeshRate.ProductRefeshRateAsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ProductRefeshRateDto>> PostAsync(CreateProductRefeshRateDto createProductRefeshRateDto)
        {
            var productRefeshRate = new ProductRefeshRate
            {
                ProductRefeshRateName = createProductRefeshRateDto.ProductRefeshRateName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productRefeshRateRepository.CreateAsync(productRefeshRate);
            return Ok(productRefeshRate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductRefeshRateDto updateProductRefeshRateDto)
        {
            var existingProductRefeshRate = await _productRefeshRateRepository.GetAsync(id);
            if (existingProductRefeshRate == null)
            {
                return NotFound();
            }
            existingProductRefeshRate.ProductRefeshRateName = updateProductRefeshRateDto.ProductRefeshRateName;
            existingProductRefeshRate.isDeleted = updateProductRefeshRateDto.isDeleted;
            existingProductRefeshRate.LatestUpdatedDate = DateTime.UtcNow;

            await _productRefeshRateRepository.UpdateAsync(existingProductRefeshRate);
            return Ok(existingProductRefeshRate);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productRefeshRate = await _productRefeshRateRepository.GetAsync(id);
            if (productRefeshRate == null) return NotFound();
            productRefeshRate.isDeleted = true;
            productRefeshRate.LatestUpdatedDate = DateTime.UtcNow;
            return Ok(productRefeshRate);
        }
    }
}
