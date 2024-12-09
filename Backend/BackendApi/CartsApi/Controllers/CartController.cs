using CartApi.Entities;
using CartsApi.Dto;
using Microsoft.AspNetCore.Mvc;
using ServicesCommon;

namespace CartsApi.Controllers
{
    [ApiController]
    [Route("Cart")]
    public class CartController : ControllerBase
    {
        private readonly IRepository<Cart> _cartRepository;

        public CartController(IRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetAsync()
        {
            var carts = (await _cartRepository.GetAllAsync())
                .Select(cart => cart.AsDto());
            return Ok(carts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CartDto>> GetByCartIdAsync(Guid id)
        {
            var cart = await _cartRepository.GetAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return cart.AsDto();
        }
        [HttpGet("userId={userid}")]
        public async Task<ActionResult<CartDto>> GetByUserIdAsync(Guid userid)
        {
            var userCart = (await _cartRepository.GetAllAsync())
                .Where(cart => cart.UserId == userid)
                .FirstOrDefault();
            if (userCart == null)
            {
                return NotFound();
            }
            return userCart.AsDto();
        }
        [HttpPost("userId={userid}")]
        public async Task<ActionResult<CartDto>> PostByUserIdAsync(Guid userid, CreateCartDto createCartDto)
        {
            var existingCart = (await _cartRepository.GetAllAsync())
                .Where(cart => cart.UserId == userid)
                .FirstOrDefault();
            if (existingCart != null)
            {
                foreach (var existingProduct in existingCart.ListProductInCart)
                {
                    foreach (var newProduct in createCartDto.ListProductInCart)
                    {
                        if (newProduct.ProductId == existingProduct.ProductId)
                        {

                        }
                    }
                }
            }
            var cart = new Cart
            {
                UserId = userid,
                ListProductInCart = createCartDto.ListProductInCart,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            await _cartRepository.CreateAsync(cart);
            return Ok(cart);
            
        }
    }
}
