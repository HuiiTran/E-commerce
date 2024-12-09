using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("Product")]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _productsRepository;
        private const int pageSize = 10;


        public ProductController(IRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }

        /// <summary>
        /// Get all product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAsync([FromQuery] int pageNumber = 1)
        {
            if (pageNumber < 1) return BadRequest();
            var products = (await _productsRepository.GetAllAsync())
                .Where(p => p.isDeleted == false)
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(product => product.AsDto());
            return Ok(products);
        }
        [HttpGet("AdminSide")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAsyncAdmin([FromQuery] int pageNumber = 1)
        {
            if (pageNumber < 1) return BadRequest();
            var products = (await _productsRepository.GetAllAsync())
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(product => product.AsDto());
            return Ok(products);
        }
        [HttpGet("PagesAmount")]
        public async Task<ActionResult<int>> GetPagesAmount()
        {
            int pagesAmount = 1;
            var totalProducts = (await _productsRepository.GetAllAsync())
                .Where (p => p.isDeleted == false)
                .Count();
            if (totalProducts > pageSize)
            {
                pagesAmount = (totalProducts % pageSize == 0) ? (totalProducts % pageSize) : (totalProducts % pageSize + 1);
            }
            if (pagesAmount == 0 || pagesAmount < 0) { pagesAmount = 1; }
            return Ok(pagesAmount);
        }
        [HttpGet("PagesAmountAdmin")]
        public async Task<ActionResult<int>> GetPagesAmountAdmin()
        {
            int pagesAmount = 1;
            var totalProducts = (await _productsRepository.GetAllAsync())
                .Count();
            if (totalProducts > pageSize)
            {
                pagesAmount = (totalProducts % pageSize == 0) ? (totalProducts % pageSize) : (totalProducts % pageSize + 1);
            }
            if (pagesAmount == 0) { pagesAmount = 1; }
            return Ok(pagesAmount);
        }
        /// <summary>
        /// Search product with keyword include in name, description, brand of products
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("Search/")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchAsync([FromQuery] string searchString, [FromQuery] int pageNumber = 1)
        {
            if (pageNumber < 1 ) return BadRequest();
         
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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetWithFilter(
                                                                               [FromQuery] string? ProductBrand, 
                                                                               [FromQuery] string? ProductType,
                                                                               [FromQuery] string? ProductOperatingSystem,
                                                                               [FromQuery] string? ProductConnectivity,
                                                                               [FromQuery] string? ProductBatteryCapacity,
                                                                               [FromQuery] string? ProductNetworkType,
                                                                               [FromQuery] string? ProductRam,
                                                                               [FromQuery] string? ProductResolution,
                                                                               [FromQuery] string? ProductRefeshRate,
                                                                               [FromQuery] string? ProductSpecialFeature,
                                                                               [FromQuery] int? PriceMin,
                                                                               [FromQuery] int? PriceMax,
                                                                               [FromQuery] string? Order, 
                                                                               [FromQuery] int pageNumber = 1
                                                                               )
        {
            if (pageNumber < 1 ) return BadRequest();
            if (PriceMax != null && PriceMin != null && PriceMax < PriceMin)
            {
                (PriceMin, PriceMax) = (PriceMax, PriceMin);
            } 
            var products = (await _productsRepository.GetAllAsync())
                .Where(p =>
                ((ProductBrand == null) ? true : p.ProductBrand == ProductBrand)
                && ((ProductType == null) ? true : p.ProductType == ProductType)
                && ((ProductOperatingSystem == null) ? true : p.ProductOperatingSystem == ProductOperatingSystem)
                && ((ProductConnectivity == null) ? true : p.ProductConnectivity == ProductConnectivity)
                && ((ProductBatteryCapacity == null) ? true : p.ProductBatteryCapacity == ProductBatteryCapacity)
                && ((ProductNetworkType == null) ? true : p.ProductNetworkType == ProductNetworkType)
                && ((ProductRam == null) ? true : p.ProductRam == ProductRam)
                && ((ProductResolution == null) ? true : p.ProductResolution == ProductResolution)
                && ((ProductRefeshRate == null) ? true : p.ProductRefeshRate == ProductRefeshRate)
                && ((ProductSpecialFeature == null) ? true : p.ProductSpecialFeature == ProductSpecialFeature)
                && ((PriceMin == null) ? true : p.ProductPrice >= PriceMin)
                && ((PriceMax == null) ? true : p.ProductPrice <= PriceMax)
                );
                
                

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
        public async Task<ActionResult<IEnumerable<ProductDto>>> FilterByProductType([FromQuery] string productType, [FromQuery] int pageNumber = 1)
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
        public async Task<ActionResult<IEnumerable<ProductDto>>> FilterByAscendingPrice([FromQuery] int pageNumber = 1)
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
        public async Task<ActionResult<IEnumerable<ProductDto>>> FilterByDescendingPrice([FromQuery] int pageNumber = 1)
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


        /// <summary>
        /// Create new Product
        /// </summary>
        /// <param name="createProductDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostAsync([FromForm]CreateProductDto createProductDto)
        {
            var product = new Product
            {
                ProductName = createProductDto.ProductName,
                ProductDescription = createProductDto.ProductDescription,
                ProductPrice = createProductDto.ProductPrice,
                ProductQuantity = createProductDto.ProductQuantity,
                ProductBrand = createProductDto.ProductBrand,
                ProductType = createProductDto.ProductType,
                ProductOperatingSystem = createProductDto.ProductOperatingSystem,
                ProductConnectivity = createProductDto.ProductConnectivity,
                ProductBatteryCapacity = createProductDto.ProductBatteryCapacity,
                ProductNetworkType = createProductDto.ProductNetworkType,
                ProductRam = createProductDto.ProductRam,
                ProductStorage = createProductDto.ProductStorage,
                ProductResolution = createProductDto.ProductResolution,
                ProductRefeshRate = createProductDto.ProductRefeshRate,
                ProductSpecialFeature = createProductDto.ProductSpecialFeature,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };

            List<string> tempImage = new List<string>();
            foreach (var image in createProductDto.ProductImages)
            {
                if (image != null)
                {
                    MemoryStream memoryStream = new MemoryStream();
                    image.OpenReadStream().CopyTo(memoryStream);
                    tempImage.Add(Convert.ToBase64String(memoryStream.ToArray()));
                }
            }
            product.ProductImages = tempImage;

            await _productsRepository.CreateAsync(product);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromForm] UpdateProductDto updateProductDto)
        {
            var existingProduct = await _productsRepository.GetAsync(id);
            List<string>? tempImages = new List<string>();
            var existingImages = existingProduct.ProductImages;

            if (existingProduct == null) return NotFound();
            existingProduct.ProductName = updateProductDto.ProductName;
            existingProduct.ProductDescription = updateProductDto.ProductDescription;
            existingProduct.ProductPrice = updateProductDto.ProductPrice;
            existingProduct.ProductQuantity = updateProductDto.ProductQuantity;
            existingProduct.ProductBrand = updateProductDto.ProductBrand;
            existingProduct.ProductType = updateProductDto.ProductType;
            existingProduct.ProductOperatingSystem = updateProductDto.ProductOperatingSystem;
            existingProduct.ProductConnectivity = updateProductDto.ProductConnectivity;
            existingProduct.ProductBatteryCapacity = updateProductDto.ProductBatteryCapacity;
            existingProduct.ProductNetworkType = updateProductDto.ProductNetworkType;
            existingProduct.ProductRam = updateProductDto.ProductRam;
            existingProduct.ProductStorage = updateProductDto.ProductStorage;
            existingProduct.ProductResolution = updateProductDto.ProductResolution;
            existingProduct.ProductRefeshRate = updateProductDto.ProductRefeshRate;
            existingProduct.ProductSpecialFeature = updateProductDto.ProductSpecialFeature;
            existingProduct.isDeleted = updateProductDto.isDeleted;
            existingProduct.DiscountPercentage = updateProductDto.DiscountPercentage;
            existingProduct.LatestUpdatedDate = DateTime.UtcNow;

            if(updateProductDto.ProductImages == null)
            {
                existingProduct.ProductImages = existingImages;
            }
            else
            {
                foreach (var image in updateProductDto.ProductImages)
                {
                    if (image != null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        image.OpenReadStream().CopyTo(memoryStream);
                        tempImages.Add(Convert.ToBase64String(memoryStream.ToArray()));
                    }
                    existingProduct.ProductImages = tempImages;
                }
            }
            await _productsRepository.UpdateAsync(existingProduct);

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var product = await _productsRepository.GetAsync(id);
            if (product != null)
            {
                product.isDeleted = true;
                product.LatestUpdatedDate = DateTimeOffset.UtcNow;
                await _productsRepository.UpdateAsync(product);
                return Ok();
            }
            return NotFound();
        }
    }
}
