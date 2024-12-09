using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductResolution")]
    public class ProductResolutionController : ControllerBase
    {
        private readonly IRepository<ProductResolution> _productResolutionRepository;

        public ProductResolutionController(IRepository<ProductResolution> productResolutionRepository)
        {
            _productResolutionRepository = productResolutionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResolutionDto>>> GetAsync()
        {
            var productResolutions = (await _productResolutionRepository.GetAllAsync())
                .Where(p => p.isDeleted == false)
                .Select(productResolution => productResolution.productResolutionAsDto());
            return Ok(productResolutions);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResolutionDto>> GetByIdAsync(Guid id) 
        {
            var productResolution = await _productResolutionRepository.GetAsync(id);
            if(productResolution == null)
            {
                return NotFound();
            }
            if (productResolution.isDeleted == true) return BadRequest();
            return productResolution.productResolutionAsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ProductResolutionDto>> PostAsync(CreateProductResolutionDto createProductResolutionDto)
        {
            var productResolution = new ProductResolution
            {
                ProductResolutionName = createProductResolutionDto.ProductResolutionName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productResolutionRepository.CreateAsync(productResolution);
            return Ok(productResolution);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductResolutionDto updateProductResolutionDto)
        {
            var existingProductResolution = await _productResolutionRepository.GetAsync(id);
            if(existingProductResolution == null)
            {
                return NotFound();
            }
            existingProductResolution.ProductResolutionName = updateProductResolutionDto.ProductResolutionName;
            existingProductResolution.isDeleted = updateProductResolutionDto.isDeleted;
            existingProductResolution.LatestUpdatedDate = DateTime.UtcNow;

            await _productResolutionRepository.UpdateAsync(existingProductResolution);
            return Ok(existingProductResolution);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productResolution = await _productResolutionRepository.GetAsync(id);
            if (productResolution == null)
            {
                return NotFound();
            }
            productResolution.isDeleted = true;
            productResolution.LatestUpdatedDate = DateTime.UtcNow;
            await _productResolutionRepository.UpdateAsync(productResolution);
            return Ok(productResolution);
        }

        [HttpGet("Admin")]
        public async Task<ActionResult<IEnumerable<ProductResolutionDto>>> GetAsyncAdmin()
        {
            var productResolutions = (await _productResolutionRepository.GetAllAsync())
                .Select(productResolution => productResolution.productResolutionAsDto());
            return Ok(productResolutions);
        }
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<ProductResolutionDto>> GetByIdAsyncAdmin(Guid id)
        {
            var productResolution = await _productResolutionRepository.GetAsync(id);
            if (productResolution == null)
            {
                return NotFound();
            }
            return productResolution.productResolutionAsDto();
        }
    }
}
