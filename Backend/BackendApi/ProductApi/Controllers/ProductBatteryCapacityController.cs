﻿using Microsoft.AspNetCore.Mvc;
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
                .Where(p => p.isDeleted == false)
                .Select(batteryCapacity => batteryCapacity.ProductBatteryCapacityAsDto());
            return Ok(productBatteryCapacity);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBatteryCapacityDto>> GetByIdAsync(Guid id)
        {
            var batteryCapacity = await _productsBatteryCapacityRepository.GetAsync(id);
            if (batteryCapacity != null)
            {
                return batteryCapacity.ProductBatteryCapacityAsDto();
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProductBatteryCapacityDto>> PostAsync(CreateProductBatteryCapacityDto createProductBatteryCapacityDto)
        {
            var batteryCapacity = new ProductBatteryCapacity
            {
                ProductBatteryCapacityName = createProductBatteryCapacityDto.ProductBatteryCapacityName,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _productsBatteryCapacityRepository.CreateAsync(batteryCapacity);
            return Ok(batteryCapacity);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id,UpdateProductBatteryCapacityDto updateProductBatteryCapacityDto)
        {
            var existingBatteryCapacity = await _productsBatteryCapacityRepository.GetAsync(id);
            if (existingBatteryCapacity != null)
            {
                existingBatteryCapacity.ProductBatteryCapacityName = updateProductBatteryCapacityDto.ProductBatteryCapacityName;
                existingBatteryCapacity.LatestUpdatedDate = DateTime.UtcNow;
                existingBatteryCapacity.isDeleted = updateProductBatteryCapacityDto.isDeleted;
                await _productsBatteryCapacityRepository.UpdateAsync(existingBatteryCapacity);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var batteryCapacity = await _productsBatteryCapacityRepository.GetAsync(id);
            if( batteryCapacity != null)
            {
                batteryCapacity.isDeleted = true;
                batteryCapacity.LatestUpdatedDate = DateTime.UtcNow;
                await _productsBatteryCapacityRepository.UpdateAsync(batteryCapacity);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("GetAllAdmin")]
        public async Task<ActionResult<IEnumerable<ProductBatteryCapacityDto>>> GetAsyncAdmin()
        {
            var productBatteryCapacity = (await _productsBatteryCapacityRepository.GetAllAsync())
                .Select(batteryCapacity => batteryCapacity.ProductBatteryCapacityAsDto());
            return Ok(productBatteryCapacity);
        }
    }
}
