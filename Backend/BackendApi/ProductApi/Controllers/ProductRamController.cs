using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductRam")]
    public class ProductRamController : ControllerBase
    {
        private IRepository<ProductRam> _productRamRepository;

        public ProductRamController(IRepository<ProductRam> productRamRepository)
        {
            _productRamRepository = productRamRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductRamDto>>> GetAsync()
        {
            var productRams = (await _productRamRepository.GetAllAsync())
                .Select(productRam => productRam.ProductRamAsDto());
            return Ok(productRams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductRamDto>> GetByIdAsync(Guid id)
        {
            var productRam = await _productRamRepository.GetAsync(id);
            if (productRam == null)
            {
                return NotFound();
            }
            return productRam.ProductRamAsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ProductRamDto>> PostAsync(CreateProductRamDto createproductRamDto)
        {
            var productRam = new ProductRam
            {
                ProductRamName = createproductRamDto.ProductRamName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productRamRepository.CreateAsync(productRam);
            return Ok(productRam);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateProductRamDto updateproductRamDto)
        {
            var existingProductRam = await _productRamRepository.GetAsync(id);
            if (existingProductRam == null)
            {
                return NotFound();
            }
            existingProductRam.ProductRamName = updateproductRamDto.ProductRamName;
            existingProductRam.isDeleted = updateproductRamDto.isDeleted;
            existingProductRam.LatestUpdatedDate = DateTime.UtcNow;

            await _productRamRepository.UpdateAsync(existingProductRam);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var productRam = await _productRamRepository.GetAsync(id);
            if (productRam == null)
            {
                return NotFound();
            }
            productRam.isDeleted = true;
            productRam.LatestUpdatedDate = DateTime.UtcNow;
            return Ok();
        }
    }
}
