using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductBrand")]
    public class ProductBrandController : ControllerBase
    {
        private readonly IRepository<ProductBrand> _productBrandRepository;

        public ProductBrandController(IRepository<ProductBrand> productBrandRepository)
        {
            _productBrandRepository = productBrandRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductBrandDto>>> GetAsync()
        {
            var productBrands = (await _productBrandRepository.GetAllAsync())
                .Select(productBrand => productBrand.ProductBrandAsDto());
            return Ok(productBrands);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrandDto>> GetByIdAsync(Guid id)
        {
            var productBrand = await _productBrandRepository.GetAsync(id);
            if (productBrand != null)
            {
                return productBrand.ProductBrandAsDto();
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<ProductBrandDto>> PostAsync(CreateProductBrandDto createProductBrandDto) 
        {
            var productBrand = new ProductBrand
            {
                ProductBrandName = createProductBrandDto.ProductBrandName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productBrandRepository.CreateAsync(productBrand);
            return Ok(productBrand);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductBrandDto updateProductBrandDto)
        {
            var existingProductBrand = await _productBrandRepository.GetAsync(id);
            if (existingProductBrand != null)
            {
                existingProductBrand.ProductBrandName = updateProductBrandDto.ProductBrandName;
                existingProductBrand.isDeleted = updateProductBrandDto.isDeleted;
                existingProductBrand.LatestUpdatedDate = DateTime.UtcNow;
                await _productBrandRepository.UpdateAsync(existingProductBrand);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productBrand = await _productBrandRepository.GetAsync(id);
            if (productBrand != null)
            {
                productBrand.isDeleted = true;
                productBrand.LatestUpdatedDate= DateTime.UtcNow;
                await _productBrandRepository.UpdateAsync(productBrand);
                return Ok();
            }
            return NotFound();
        }
    }
}
