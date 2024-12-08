using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductConnectivity")]
    public class ProductConnectivityController : ControllerBase
    {
        private readonly IRepository<ProductConnectivity> _productConnectivityRepository;
        
        public ProductConnectivityController(IRepository<ProductConnectivity> productConnectivityRepository)
        {
            _productConnectivityRepository = productConnectivityRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductConnectivityDto>>> GetAsync()
        {
            var productConnectivities = (await _productConnectivityRepository.GetAllAsync())
                .Select(productConnectivity => productConnectivity.ProductConnectivityAsDto());
            return Ok(productConnectivities);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductConnectivityDto>> GetByIdAsync(Guid id)
        {
            var productConnectivity = await _productConnectivityRepository.GetAsync(id);
            if(productConnectivity != null)
            {
                return productConnectivity.ProductConnectivityAsDto();
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult<ProductConnectivityDto>> PostAsync(CreateProductConnectivityDto createProductConnectivityDto)
        {
            var productConnectivity = new ProductConnectivity
            {
                ProductConnectivityName = createProductConnectivityDto.ProductConnectivityName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productConnectivityRepository.CreateAsync(productConnectivity);
            return Ok(productConnectivity);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id,UpdateProductConnectivityDto updateProductConnectivityDto)
        {
            var existingProductConnectivity = await _productConnectivityRepository.GetAsync(id);
            if(existingProductConnectivity != null)
            {
                existingProductConnectivity.ProductConnectivityName = updateProductConnectivityDto.ProductConnectivityName;
                existingProductConnectivity.isDeleted = updateProductConnectivityDto.isDeleted;
                existingProductConnectivity.LatestUpdatedDate = DateTime.UtcNow;
                await _productConnectivityRepository.UpdateAsync(existingProductConnectivity);
                return Ok(existingProductConnectivity);
            }
            return NotFound();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productConnectivity = await _productConnectivityRepository.GetAsync(id);
            if (productConnectivity != null)
            {
                productConnectivity.isDeleted = true;
                productConnectivity.LatestUpdatedDate = DateTime.UtcNow;
                await _productConnectivityRepository.UpdateAsync(productConnectivity);
                return Ok();
            }
            return NotFound();
        }
    }
}
