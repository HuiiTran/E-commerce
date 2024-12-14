using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Dtos;
using ProductApi.Entities;
using ServicesCommon;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("ImportProductsBill")]
    public class ImportProductsController : ControllerBase
    {
        private readonly IRepository<ImportProductBill> _productsBillRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<User> _userRepository;
        public ImportProductsController(IRepository<ImportProductBill> billRepository, IRepository<Product> productRepository, IRepository<User> userRepository)
        {
            _productsBillRepository = billRepository;
            _productsRepository = productRepository;
            _userRepository = userRepository;
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
                totalPrice += product.Price;
            }
            ImportProductBill importProductBill = new ImportProductBill
            {
                ListImportProducts = createImportProductBillsDto.ImportProducts.ToList(),
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
