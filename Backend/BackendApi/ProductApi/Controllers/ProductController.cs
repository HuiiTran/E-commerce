using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productsRepository;
        
        public ProductController(IRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAsync()
        {
            var products = (await _productsRepository.GetAllAsync())
                            .Select(product => product.AsDto());
            return Ok(products);
        }
        /// <summary>
        /// Search product with keyword include in name, description, brand of products
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("Search/")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchAsync([FromQuery] string searchString, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1) return BadRequest();
         
            var products = (await _productsRepository.GetAllAsync())
                .Where(p => p.ProductName.Contains(searchString) || p.ProductDescription.Contains(searchString) || p.ProductBrand.Contains(searchString ))
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(product => product.AsDto());

            if (products != null) return Ok(products);                

            return NotFound();

        }
        
        [HttpGet("Filter/")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetWithFilter([FromQuery] string searchString, 
                                                                               [FromQuery] string ProductBrand, 
                                                                               [FromQuery] string ProductType,
                                                                               [FromQuery] string ProductOperatingSystem,
                                                                               [FromQuery] string ProductConnectivity,
                                                                               [FromQuery] string ProductBatteryCapacity,
                                                                               [FromQuery] string ProductNetworkType,
                                                                               [FromQuery] string ProductRam,
                                                                               [FromQuery] string ProductResolution,
                                                                               [FromQuery] string ProductRefeshRate,
                                                                               [FromQuery] string ProductSpecialFeature,
                                                                               [FromQuery] int PriceMin,
                                                                               [FromQuery] int PriceMax,
                                                                               [FromQuery] string Order, 
                                                                               [FromQuery] int pageNumber = 1, 
                                                                               [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1) return BadRequest();
            var products = (await _productsRepository.GetAllAsync())
                .Where(p => p.ProductName.Contains(searchString) || p.ProductDescription.Contains(searchString) || p.ProductBrand.Contains(searchString))
                .Where(p => ProductBrand != null && p.ProductBrand == ProductBrand)
                .Where(p => ProductType != null && p.ProductType == ProductType)
                .Where(p => ProductOperatingSystem != null && p.ProductOperatingSystem == ProductOperatingSystem)
                .Where(p => ProductConnectivity != null && p.ProductConnectivity == ProductConnectivity)
                .Where(p => ProductBatteryCapacity != null && p.ProductBatteryCapacity == ProductBatteryCapacity)
                .Where(p => ProductNetworkType != null && p.ProductNetworkType == ProductNetworkType)
                .Where(p => ProductRam != null && p.ProductRam == ProductRam)
                .Where(p => ProductResolution != null && p.ProductResolution == ProductResolution)
                .Where(p => ProductRefeshRate != null && p.ProductRefeshRate == ProductRefeshRate)
                .Where(p => ProductSpecialFeature != null && p.ProductSpecialFeature == ProductSpecialFeature)
                .Where(p => p.ProductPrice >= PriceMin && p.ProductPrice <= PriceMax);

            if (products == null) return NotFound();

            if (Order == "Ascending")
            {
                var product = products.OrderBy(p => p.ProductPrice)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(product => product.AsDto());
                return Ok(product);
            }
            else
            {
                var product = products.OrderByDescending(p => p.ProductPrice)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(product => product.AsDto());
                return Ok(product);
            }
        }
        /// <summary>
        /// Get all product with the same product's type
        /// </summary>
        /// <param name="productType"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("ByType/")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> FilterByProductType([FromQuery] string productType, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1) return BadRequest();

            var products = (await _productsRepository.GetAllAsync())
                .Where(p => p.ProductType.Contains(productType))
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(product => product.AsDto());
            if (products != null) return Ok(products);

            return NotFound();
        }

        /// <summary>
        /// Get all product in price ascending order 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("ByAscendingPrice/")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> FilterByAscendingPrice([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1) return BadRequest();

            var products = (await _productsRepository.GetAllAsync())
                .OrderBy(p => p.ProductPrice)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(product => product.AsDto());
            if (products != null) return Ok(products);

            return NotFound();
        }

        /// <summary>
        /// Get all product in price descending order 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("ByDescendingPrice/")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> FilterByDescendingPrice([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1) return BadRequest();

            var products = (await _productsRepository.GetAllAsync())
                .OrderByDescending(p => p.ProductPrice)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(product => product.AsDto());
            if (products != null) return Ok(products);

            return NotFound();
        }

        /// <summary>
        /// Get information of a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<ProductDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var product = await _productsRepository.GetAsync(id);

            if (product == null) return NotFound();
            return product.AsDto();
        }
    }
}
