using BillApi.Dto;
using BillApi.Entities;
using MassTransit.Initializers;
using Messages;
using Microsoft.AspNetCore.Mvc;
using ServicesCommon;

namespace BillApi.Controllers
{
    [ApiController]
    [Route("Bill")]
    public class BillController : ControllerBase
    {
        private readonly IRepository<Bill> _billRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Staff> _staffRepository;
        private readonly IRepository<Product> _productRepository;
        private CustomMessages customMessages = new CustomMessages();
        public BillController(IRepository<Bill> billRepository, IRepository<User> userRepository, IRepository<Staff> staffRepository, IRepository<Product> productRepository)
        {
            _billRepository = billRepository;
            _userRepository = userRepository;
            _staffRepository = staffRepository;
            _productRepository = productRepository;
        }

        [HttpGet("ListBill")]
        public async Task<ActionResult<IEnumerable<ListBillDto>>> GetAllListBill()
        {
            var listBills = (await _billRepository.GetAllAsync())
                .Select(async bill =>
                {
                    var OwerName = await _userRepository.GetAsync(bill.OwnerCreatedId)
                                    .Select(user => user.Name);
                    if (OwerName == null)
                    {
                        OwerName = await _staffRepository.GetAsync(bill.OwnerCreatedId)
                                        .Select(staff => staff.Name);
                    }
                    return bill.ListBillAsDto(OwerName);
                });
            return Ok(listBills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillDto>> GetBillById(Guid id)
        {
            List<Product> listProducts = new List<Product>();
            var bill = await _billRepository.GetAsync(id);
            if (bill == null) return NotFound();
            foreach (var product in bill.ListProductInBill)
            {
                var productItem = await _productRepository.GetAsync(product.ProductId);
                if (productItem == null) continue;
                listProducts.Add(productItem);
            }
            string OwnerName = await _userRepository.GetAsync(bill.OwnerCreatedId)
                .Select(owner => owner.Name);

            return bill.BillAsDto(listProducts, OwnerName);
        }
        [HttpGet("statisticBetween")]
        public async Task<ActionResult<decimal>> GetStatisticBetweenAsync([FromQuery] int startMonth, [FromQuery] int endMonth, [FromQuery] int year)
        {
            if (startMonth < 1 && startMonth > 12) return BadRequest(customMessages.MSG_06);
            if (endMonth < 1 && endMonth > 12) return BadRequest(customMessages.MSG_07);
            if (startMonth > endMonth) return BadRequest(customMessages.MSG_08);
            decimal totalPriceInPeriod = 0;
            var bills = (await _billRepository.GetAllAsync())
                .Where(b => (b.CreatedDate.Month <= endMonth && b.CreatedDate.Month >= startMonth && b.CreatedDate.Year == year));
            foreach (var bill in bills)
            {
                totalPriceInPeriod += bill.TotalPrice;
            }
            return Ok(totalPriceInPeriod);
        }
        [HttpGet("statisticMonth")]
        public async Task<ActionResult<decimal>> GetStatisticMonth([FromQuery] int month, [FromQuery] int year)
        {
            if (month < 1 && month > 12) return BadRequest(customMessages.MSG_09);
            decimal totalPriceInMonth = 0;
            var bills = (await _billRepository.GetAllAsync())
                .Where(b => b.CreatedDate.Month == month && b.CreatedDate.Year == year);
            foreach (var bill in bills)
            {
                totalPriceInMonth += bill.TotalPrice;
            }
            return Ok(totalPriceInMonth);
        }
        [HttpGet("statisticQuarter")]
        public async Task<ActionResult<decimal>> GetStatisticQuarter([FromQuery] int quarter, [FromQuery] int year)
        {
            if (quarter < 1 && quarter > 4) return BadRequest(customMessages.MSG_10);
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
                    return BadRequest(customMessages.MSG_10);
            }

            decimal totalPriceInQuarter = 0;
            var bills = (await _billRepository.GetAllAsync())
                .Where(b => (b.CreatedDate.Month <= endMonth && b.CreatedDate.Month >= startMonth && b.CreatedDate.Year == year));
            foreach (var bill in bills)
            {
                totalPriceInQuarter += bill.TotalPrice;
            }
            return Ok(totalPriceInQuarter);
        }
        [HttpPost]
        public async Task<ActionResult<BillDto>> PostAsync([FromQuery] Guid OwnerId, CreateBillDto createBillDto)
        {
            decimal totalPrice = 0;
            foreach(var product in createBillDto.ListProductInBill)
            {
                var itemInfor = await _productRepository.GetAsync(product.ProductId);
                totalPrice += itemInfor.ProductPrice * product.Quantity;
            }

            string ownerName = await _userRepository.GetAsync(OwnerId)
                .Select(user => user.Name);
            if(ownerName == null)
            {
                ownerName = await _staffRepository.GetAsync(OwnerId)
                    .Select(staff => staff.Name);
            }
            foreach(var item in  createBillDto.ListProductInBill)
            {
                if (item.Quantity == 0)
                    return BadRequest(customMessages.MSG_11);
            }
            Bill bill = new Bill
            {
                OwnerCreatedId = OwnerId,
                ListProductInBill = createBillDto.ListProductInBill,
                TotalPrice = totalPrice,
                PhoneNumber = createBillDto.PhoneNumber,
                Address = createBillDto.Address,
                Status = "Pending",
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            await _billRepository.CreateAsync(bill);
            return Ok(customMessages.MSG_17);
        }
        [HttpPut("updateStatus{id}")]
        public async Task<IActionResult> UdpateStatus(Guid id, UpdateBillDto updateBillDto) 
        {
            var existingBill = await _billRepository.GetAsync(id);

            if (existingBill == null)
            {
                return NotFound(customMessages.MSG_01);
            }
            existingBill.Status = updateBillDto.Status;
            existingBill.UpdatedDate = DateTime.UtcNow;

            await _billRepository.UpdateAsync(existingBill);

            return Ok(customMessages.MSG_18);
        }
    }
}
