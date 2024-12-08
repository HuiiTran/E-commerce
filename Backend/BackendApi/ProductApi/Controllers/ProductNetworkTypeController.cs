using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductNetworkType")]
    public class ProductNetworkTypeController : ControllerBase
    {
        private readonly IRepository<ProductNetworkType> _productNetworkTypeRepository;

        public ProductNetworkTypeController(IRepository<ProductNetworkType> productNetworkTypeRepository)
        {
            _productNetworkTypeRepository = productNetworkTypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductNetworkTypeDto>>> GetAsync()
        {
            var productNetworkTypes = (await _productNetworkTypeRepository.GetAllAsync())
                .Select(productNetworkType => productNetworkType.ProductNetworkTypeAsDto());
            return Ok(productNetworkTypes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductNetworkTypeDto>> GetByIdAsync(Guid id)
        {
            var productNetworkType = await _productNetworkTypeRepository.GetAsync(id);
            if(productNetworkType != null)
            {
                return productNetworkType.ProductNetworkTypeAsDto();
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<ProductNetworkTypeDto>> PostAsync(CreateProductNetworkTypeDto createProductNetworkTypeDto)
        {
            var productNetworkType = new ProductNetworkType
            {
                ProductNetworkTypeName = createProductNetworkTypeDto.ProductNetworkTypeName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productNetworkTypeRepository.CreateAsync(productNetworkType);
            return Ok(productNetworkType);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductNetworkTypeDto updateProductNetworkTypeDto)
        {
            var existingProductNetworkType = await _productNetworkTypeRepository.GetAsync(id);
            if(existingProductNetworkType != null)
            {
                existingProductNetworkType.ProductNetworkTypeName = updateProductNetworkTypeDto.ProductNetworkTypeName;
                existingProductNetworkType.isDeleted = updateProductNetworkTypeDto.isDeleted;
                existingProductNetworkType.LatestUpdatedDate = DateTime.UtcNow;
                await _productNetworkTypeRepository.UpdateAsync(existingProductNetworkType);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productNetworkType = await _productNetworkTypeRepository.GetAsync(id);
            if(productNetworkType != null)
            {
                productNetworkType.isDeleted = true;
                productNetworkType.LatestUpdatedDate= DateTime.UtcNow;
                await _productNetworkTypeRepository.UpdateAsync(productNetworkType);
                return Ok();
            }
            return NotFound();
        }
    } 
}
