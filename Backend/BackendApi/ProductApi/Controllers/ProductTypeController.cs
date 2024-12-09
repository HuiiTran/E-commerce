using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductType")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IRepository<ProductType> _productTypeRepository;
        public ProductTypeController(IRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTypeDto>>> GetAsync()
        {
            var productTypes = (await _productTypeRepository.GetAllAsync())
                .Select(productType => productType.ProductTypeAsDto());
            return Ok(productTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeDto>> GetByIdAsync(Guid id)
        {
            var productType = await _productTypeRepository.GetAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return productType.ProductTypeAsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ProductTypeDto>> PostAsync(CreateProductTypeDto createProductTypeDto)
        {
            var productType = new ProductType
            {
                TypeName = createProductTypeDto.TypeName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productTypeRepository.CreateAsync(productType);
            return Ok(productType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductTypeDto updateProductTypeDto)
        {
            var existingProductType = await _productTypeRepository.GetAsync(id);
            if (existingProductType == null)
            {
                return NotFound();
            }
            existingProductType.TypeName = updateProductTypeDto.TypeName;
            existingProductType.isDeleted = updateProductTypeDto.isDeleted;
            existingProductType.LatestUpdatedDate = DateTime.UtcNow;

            await _productTypeRepository.UpdateAsync(existingProductType);
            return Ok(existingProductType);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productType = await _productTypeRepository.GetAsync(id);
            if (productType == null) return NotFound();

            productType.isDeleted = true;
            productType.LatestUpdatedDate = DateTime.UtcNow;

            await _productTypeRepository.UpdateAsync(productType);
            return Ok(productType);
        }
    }
}
