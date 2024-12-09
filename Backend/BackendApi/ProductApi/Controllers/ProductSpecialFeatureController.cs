using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductSpecialFeature")]
    public class ProductSpecialFeatureController : ControllerBase
    {
        private readonly IRepository<ProductSpecialFeature> _productSpecialFeatureRepository;

        public ProductSpecialFeatureController(IRepository<ProductSpecialFeature> productSpecialFeatureRepository)
        {
            _productSpecialFeatureRepository = productSpecialFeatureRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSpecialFeatureDto>>> GetAsync()
        {
            var productSpecialFeatures = (await _productSpecialFeatureRepository.GetAllAsync())
                .Where(p => p.isDeleted == false)
                .Select(productSpecialFeature => productSpecialFeature.ProductSpecialFeatureAsDto());
            return Ok(productSpecialFeatures);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSpecialFeatureDto>> GetByIdAsync(Guid id)
        {
            var productSpecialFeatures = await _productSpecialFeatureRepository.GetAsync(id);
            if (productSpecialFeatures == null)
            {
                return NotFound();
            }
            if (productSpecialFeatures.isDeleted == true) return BadRequest();
            return productSpecialFeatures.ProductSpecialFeatureAsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ProductSpecialFeatureDto>> PostAsync(CreateProductSpecialFeatureDto createProductSpecialFeatureDto)
        {
            var productSpecialFeatures = new ProductSpecialFeature
            {
                ProductSpecialFeatureName = createProductSpecialFeatureDto.ProductSpecialFeatureName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productSpecialFeatureRepository.CreateAsync(productSpecialFeatures);
            return Ok(productSpecialFeatures);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductSpecialFeatureDto updateProductSpecialFeatureDto)
        {
            var existingProductSpecialFeatures = await _productSpecialFeatureRepository.GetAsync(id);
            if(existingProductSpecialFeatures == null)
            {
                return NotFound();
            }
            existingProductSpecialFeatures.ProductSpecialFeatureName = updateProductSpecialFeatureDto.ProductSpecialFeatureName;
            existingProductSpecialFeatures.isDeleted = updateProductSpecialFeatureDto.isDeleted;
            existingProductSpecialFeatures.LatestUpdatedDate = DateTime.UtcNow;

            await _productSpecialFeatureRepository.UpdateAsync(existingProductSpecialFeatures);
            return Ok(existingProductSpecialFeatures);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productSpecialFeatures = await _productSpecialFeatureRepository.GetAsync(id);
            if (productSpecialFeatures == null)
            {
                return NotFound();
            }
            productSpecialFeatures.isDeleted = true;
            productSpecialFeatures.LatestUpdatedDate = DateTime.UtcNow;

            await _productSpecialFeatureRepository.UpdateAsync(productSpecialFeatures);
            return Ok(productSpecialFeatures);
        }

        [HttpGet("Admin")]
        public async Task<ActionResult<IEnumerable<ProductSpecialFeatureDto>>> GetAsyncAdmin()
        {
            var productSpecialFeatures = (await _productSpecialFeatureRepository.GetAllAsync())
                .Select(productSpecialFeature => productSpecialFeature.ProductSpecialFeatureAsDto());
            return Ok(productSpecialFeatures);
        }
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<ProductSpecialFeatureDto>> GetByIdAsyncAdmin(Guid id)
        {
            var productSpecialFeatures = await _productSpecialFeatureRepository.GetAsync(id);
            if (productSpecialFeatures == null)
            {
                return NotFound();
            }
            return productSpecialFeatures.ProductSpecialFeatureAsDto();
        }

    }
}
