using MassTransit;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ProductContract;
using ServicesCommon;
using System.ComponentModel;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ImportProductsBill")]
    public class ImportProductsController : ControllerBase
    {
        private readonly IRepository<ImportProductBill> _productsBillRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        public ImportProductsController(IRepository<ImportProductBill> billRepository, IRepository<Product> productRepository, IRepository<User> userRepository, IPublishEndpoint publishEndpoint)
        {
            _productsBillRepository = billRepository;
            _productsRepository = productRepository;
            _userRepository = userRepository;
            _publishEndpoint = publishEndpoint;
        }


        [HttpGet("isCanceled")]
        public async Task<ActionResult<IEnumerable<AllImportProductBillDto>>> GetCanceledAsync()
        {
            var bills = (await _productsBillRepository.GetAllAsync())
                .Where(bill => bill.IsDeleted == true)
                .Select(bill => bill.AllImportProductBillAsDto());
            return Ok(bills);
        }
        [HttpGet("notCanceled")]
        public async Task<ActionResult<IEnumerable<AllImportProductBillDto>>> GetNotCanceledAsync()
        {
            var bills = (await _productsBillRepository.GetAllAsync())
                .Where(bill => bill.IsDeleted == false)
                .Select(bill => bill.AllImportProductBillAsDto());
            return Ok(bills);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<AllImportProductBillDto>>> GetAsyncAll()
        {
            var bills = (await _productsBillRepository.GetAllAsync())
                .Select(bill => bill.AllImportProductBillAsDto());
            return Ok(bills);
        }
        [HttpGet("statisticBetween")]
        public async Task<ActionResult<decimal>> GetStatisticBetweenAsync([FromQuery]int startMonth, [FromQuery] int endMonth, [FromQuery] int year)
        {
            if (startMonth < 1 && startMonth > 12) return BadRequest();
            if (endMonth < 1 && endMonth > 12) return BadRequest();
            if (startMonth > endMonth) return BadRequest();
            decimal totalPriceInPeriod = 0;
            var bills = (await _productsBillRepository.GetAllAsync())
                .Where(b => (b.CreatedDate.Month <= endMonth && b.CreatedDate.Month >= startMonth && b.CreatedDate.Year == year));
            foreach (var bill in bills)
            {
                totalPriceInPeriod += bill.Total;
            }
            return Ok(totalPriceInPeriod);
        }
        [HttpGet("statisticMonth")]
        public async Task<ActionResult<decimal>> GetStatisticMonth([FromQuery]int month, [FromQuery]int year)
        {
            if (month < 1 && month > 12) return BadRequest();
            decimal totalPriceInMonth = 0;
            var bills = (await _productsBillRepository.GetAllAsync())
                .Where(b => b.CreatedDate.Month == month && b.CreatedDate.Year == year);
            foreach(var bill in bills)
            {
                totalPriceInMonth += bill.Total;
            }
            return Ok(totalPriceInMonth);
        }
        [HttpGet("statisticQuarter")]
        public async Task<ActionResult<decimal>> GetStatisticQuarter([FromQuery] int quarter, [FromQuery]int year)
        {
            if (quarter < 1 && quarter > 4) return BadRequest();
            var startMonth = 0;
            var endMonth = 0;
            switch (quarter)
            {
                case 1:
                    startMonth = 1;
                    endMonth = 3;
                    break;
                case 2: 
                    startMonth = 4; 
                    endMonth = 6; 
                    break;
                case 3:
                    startMonth = 7;
                    endMonth = 9;
                    break;
                case 4:
                    startMonth = 10;
                    endMonth = 12;
                    break;
                default: 
                    return BadRequest();
            }

            decimal totalPriceInQuarter = 0;
            var bills = (await _productsBillRepository.GetAllAsync())
                .Where(b => (b.CreatedDate.Month <= endMonth && b.CreatedDate.Month >= startMonth && b.CreatedDate.Year == year));
            foreach (var bill in bills)
            {
                totalPriceInQuarter += bill.Total;
            }
            return Ok(totalPriceInQuarter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImportProductBillsDto>> GetByIdAsync(Guid id)
        {
            var bill = await _productsBillRepository.GetAsync(id);
            List<Product> productList = new List<Product>();
            foreach (var billItem in bill.ListImportProducts)
            {
                var product = await _productsRepository.GetAsync(billItem.ProductId);
                productList.Add(product);
            }
            string personName = await _userRepository.GetAsync(bill.PersonId)
                .Select(person => person.Name);
            return bill.importProductBillsAsDto(productList, personName);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(CreateImportProductBillsDto createImportProductBillsDto)
        {
            decimal totalPrice = 0;
            foreach(var product in createImportProductBillsDto.ImportProducts)
            {
                var exsitingProduct = await _productsRepository.GetAsync(product.ProductId);
                if(exsitingProduct == null)
                {
                    return NotFound();
                }
                exsitingProduct.ProductQuantity += product.Quantity;
                await _productsRepository.UpdateAsync(exsitingProduct);
                await _publishEndpoint.Publish(new ProductUpdate(exsitingProduct.Id,
                                                             exsitingProduct.ProductName,
                                                             exsitingProduct.ProductPrice,
                                                             exsitingProduct.DiscountPercentage,
                                                             exsitingProduct.ProductQuantity,
                                                             exsitingProduct.ProductImages[0]));
                totalPrice += product.Price;
            }
            ImportProductBill importProductBill = new ImportProductBill
            {
                ListImportProducts = createImportProductBillsDto.ImportProducts.ToList(),
                PersonId = createImportProductBillsDto.personId,
                Total = totalPrice,
                CreatedDate = DateTime.UtcNow,
            };

            
            await _productsBillRepository.CreateAsync(importProductBill);
            return Ok();
        }
        [HttpPut("CancelBill{id}")]
        public async Task<IActionResult> CancelBill(Guid id)
        {
            var bill = await _productsBillRepository.GetAsync(id);
            if(bill == null) return NotFound();
            bill.IsDeleted = true;

            await _productsBillRepository.UpdateAsync(bill);

            return Ok();
        }

        [HttpPut("UndoCancelBill{id}")]
        public async Task<IActionResult> UndoCancelBill(Guid id)
        {
            var bill = await _productsBillRepository.GetAsync(id);
            if (bill == null) return NotFound();
            bill.IsDeleted = false;

            await _productsBillRepository.UpdateAsync(bill);

            return Ok();
        }
    }
}
