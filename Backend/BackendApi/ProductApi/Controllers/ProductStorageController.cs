using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductStorage")]
    public class ProductStorageController : ControllerBase
    {
        private readonly IRepository<ProductStorage> _productStorageRepository;

        public ProductStorageController(IRepository<ProductStorage> productStorageRepository)
        {
            _productStorageRepository = productStorageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductStorageDto>>> GetAsync()
        {
            var productStorages = (await _productStorageRepository.GetAllAsync())
                .Where(p => p.isDeleted == false)
                .Select(productStorage => productStorage.ProductStorageAsDto());
            return Ok(productStorages);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductStorageDto>> GetByIdAsync(Guid id)
        {
            var productStorage = await _productStorageRepository.GetAsync(id);
            if (productStorage == null) return NotFound();
            if (productStorage.isDeleted == true) return BadRequest();
            return productStorage.ProductStorageAsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ProductStorageDto>> PostAsync(CreateProductStorageDto createProductStorageDto)
        {
            var productStorage = new ProductStorage
            {
                ProductStorageName = createProductStorageDto.ProductStorageName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };

            await _productStorageRepository.CreateAsync(productStorage);
            return Ok(productStorage);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductStorageDto updateProductStorageDto)
        {
            var existingProductStorage = await _productStorageRepository.GetAsync(id);
            if(existingProductStorage == null) return NotFound();
            existingProductStorage.ProductStorageName = updateProductStorageDto.ProductStorageName;
            existingProductStorage.isDeleted = updateProductStorageDto.isDeleted;
            existingProductStorage.LatestUpdatedDate = DateTime.UtcNow;

            await _productStorageRepository.UpdateAsync(existingProductStorage);
            return Ok(existingProductStorage);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productStorage = await _productStorageRepository.GetAsync(id);
            if(productStorage == null) return NotFound();
            productStorage.isDeleted = true;
            productStorage.LatestUpdatedDate = DateTime.UtcNow;

            await _productStorageRepository.UpdateAsync(productStorage);
            return Ok(productStorage);
        }

        [HttpGet("Admin")]
        public async Task<ActionResult<IEnumerable<ProductStorageDto>>> GetAsyncAdmin()
        {
            var productStorages = (await _productStorageRepository.GetAllAsync())
                .Select(productStorage => productStorage.ProductStorageAsDto());
            return Ok(productStorages);
        }
        [HttpGet("Admin/{id}")]
        public async Task<ActionResult<ProductStorageDto>> GetByIdAsyncAdmin(Guid id)
        {
            var productStorage = await _productStorageRepository.GetAsync(id);
            if (productStorage == null) return NotFound();
            return productStorage.ProductStorageAsDto();
        }
    }
}
