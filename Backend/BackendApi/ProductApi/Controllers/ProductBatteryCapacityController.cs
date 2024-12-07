using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ProductBatteryCapacity")]
    public class ProductBatteryCapacityController : ControllerBase
    {
        private readonly IRepository<ProductBatteryCapacity> _productsBatteryCapacityRepository;

        public ProductBatteryCapacityController(IRepository<ProductBatteryCapacity> productsBatteryCapacityRepository)
        {
            _productsBatteryCapacityRepository = productsBatteryCapacityRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductBatteryCapacityDto>>> GetAsync()
        {
            var productBatteryCapacity = (await _productsBatteryCapacityRepository.GetAllAsync())
                .Select(batteryCapacity => batteryCapacity.ProductBatteryCapacityDto());
            return Ok(productBatteryCapacity);
        }
    }
}
