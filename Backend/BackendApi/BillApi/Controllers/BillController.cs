using BillApi.Dto;
using BillApi.Entities;
using MassTransit.Initializers;
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

        [HttpPost]
        public async Task<ActionResult<BillDto>> PostAsync([FromQuery] Guid OwnerId, CreateCartDto createCartDto)
        {
            decimal totalPrice = 0;
            foreach(var product in createCartDto.ListProductInBill)
            {
                var itemInfor = await _productRepository.GetAsync(product.ProductId);
                totalPrice += itemInfor.ProductPrice;
            }

            string ownerName = await _userRepository.GetAsync(OwnerId)
                .Select(user => user.Name);
            if(ownerName == null)
            {
                ownerName = await _staffRepository.GetAsync(OwnerId)
                    .Select(staff => staff.Name);
            }

            Bill bill = new Bill
            {
                OwnerCreatedId = OwnerId,
                ListProductInBill = createCartDto.ListProductInBill,
                TotalPrice = totalPrice,
                PhoneNumber = createCartDto.PhoneNumber,
                Address = createCartDto.Address,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            await _billRepository.CreateAsync(bill);
            return Ok();
        }
    }
}
