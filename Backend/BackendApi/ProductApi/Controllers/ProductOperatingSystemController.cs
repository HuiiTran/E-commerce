using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductOperatingSystem")]
    public class ProductOperatingSystemController : ControllerBase
    {
        private readonly IRepository<ProductOperatingSystem> _productOperatingSystemRepository;

        public ProductOperatingSystemController(IRepository<ProductOperatingSystem> productOperatingSystemRepository)
        {
            _productOperatingSystemRepository = productOperatingSystemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductOperatingSystemDto>>> GetAsync()
        {
            var productOperatingSystems = (await _productOperatingSystemRepository.GetAllAsync())
                .Select(productOperatingSystem => productOperatingSystem.ProductOperatingSystemAsDto());
            return Ok(productOperatingSystems);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductOperatingSystemDto>> GetByIdAsync(Guid id)
        {
            var productOperatingSystem = await _productOperatingSystemRepository.GetAsync(id);
            if (productOperatingSystem == null)
            {
                return NotFound();
            }
            return productOperatingSystem.ProductOperatingSystemAsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ProductOperatingSystemDto>> PostAsync(CreateProductOperatingSystemDto createProductOperatingSystemDto)
        {
            var productOperatingSystem = new ProductOperatingSystem
            {
                ProductOperatingSystemName = createProductOperatingSystemDto.ProductOperatingSystemName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productOperatingSystemRepository.CreateAsync(productOperatingSystem);
            return Ok(productOperatingSystem);  
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductOperatingSystemDto updateProductOperatingSystemDto)
        {
            var existingProductOperatingSystem = await _productOperatingSystemRepository.GetAsync(id);
            if (existingProductOperatingSystem == null)
            {
                return NotFound();
            }
            existingProductOperatingSystem.ProductOperatingSystemName = updateProductOperatingSystemDto.ProductOperatingSystemName;
            existingProductOperatingSystem.isDeleted = updateProductOperatingSystemDto.isDeleted;
            existingProductOperatingSystem.LatestUpdatedDate = DateTime.UtcNow;
            return Ok(existingProductOperatingSystem);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productOperatingSystem = await _productOperatingSystemRepository.GetAsync(id);
            if (productOperatingSystem == null)
            {
                return NotFound();
            }
            productOperatingSystem.isDeleted = true;
            productOperatingSystem.LatestUpdatedDate = DateTime.UtcNow;
            return Ok(productOperatingSystem);
        }
    }
}
