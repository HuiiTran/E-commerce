using CartApi.Entities;
using CartsApi.Dto;
using CartsApi.Entities;
using Messages;
using Microsoft.AspNetCore.Mvc;
using ServicesCommon;
using ZstdSharp.Unsafe;

namespace CartsApi.Controllers
{
    [ApiController]
    [Route("Cart")]
    public class CartController : ControllerBase
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<User> _userRepository;
        private CustomMessages customMessages = new CustomMessages();

        public CartController(IRepository<Cart> cartRepository, IRepository<Product> productRepository, IRepository<User> userRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartDto>>> GetAsync()
        {
            var carts = (await _cartRepository.GetAllAsync())
                .Select(cart => cart.AsDto());
            return Ok(carts);
        }
        [HttpGet("GetUserBaseCart{userId}")]
        public async Task<ActionResult<CartDto>> GetUserBaseCart(Guid userId)
        {
            var cart = (await _cartRepository.GetAllAsync())
                .Where(c => c.UserId == userId)
                .FirstOrDefault();
            if (cart == null) return NotFound(customMessages.MSG_01);
            return cart.AsDto();
        }
        [HttpGet("allCart")]
        public async Task<ActionResult<IEnumerable<SingleCartDto>>> GetAllCartAsync()
        {
            var carts = (await _cartRepository.GetAllAsync())
                .Select(cart => cart.AsDto());
            return Ok(carts);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CartInformationDto>> GetByCartIdAsync(Guid id)
        {
            List<Product> CartProducts = new List<Product>();
            decimal totalPrice = 0;
            var cart = (await _cartRepository.GetAsync(id));
            if (cart == null)
            {
                return NotFound(customMessages.MSG_01);
            }
            var user = (await _userRepository.GetAsync(cart.UserId));
                
            foreach(var cartProduct in cart.ListProductInCart)
            {
                var product = await _productRepository.GetAsync(cartProduct.ProductId);
                if (product == null)
                {
                    continue;
                }
                CartProducts.Add(product);
                totalPrice += product.ProductPrice - (product.ProductPrice * product.DiscountPrecentage)/ 100;
            }

            return cart.CartInformationAsDto(CartProducts, user.userName, totalPrice);
        }
        [HttpGet("userId={userid}")]
        public async Task<ActionResult<CartInformationDto>> GetByUserIdAsync(Guid userid)
        {
            List<Product> CartProducts = new List<Product>();
            decimal totalPrice = 0;
            var cart = (await _cartRepository.GetAllAsync())
                .Where(cart => cart.UserId == userid)
                .FirstOrDefault();
            if (cart == null)
            {
                return NotFound();
            }
            var user = (await _userRepository.GetAsync(cart.UserId));

            foreach (var cartProduct in cart.ListProductInCart)
            {
                var product = await _productRepository.GetAsync(cartProduct.ProductId);
                if (product == null)
                {
                    continue;
                }
                CartProducts.Add(product);
                totalPrice += product.ProductPrice - (product.ProductPrice * product.DiscountPrecentage) / 100;
            }

            return cart.CartInformationAsDto(CartProducts, user.userName, totalPrice);
        }
        [HttpPost("userId={userid}")]
        public async Task<ActionResult<CartDto>> PostByUserIdAsync(Guid userid, CreateCartDto createCartDto)
        {
            foreach(var newProduct in createCartDto.ListProductInCart)
            {
                if (newProduct.Quantity < 1)
                    return BadRequest(customMessages.MSG_12);
            }

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
                            
                            existingProduct.Quantity += newProduct.Quantity;
                        }
                        else
                        {
                            existingCart.ListProductInCart.Add(newProduct);
                        }
                    }
                }
                await _cartRepository.UpdateAsync(existingCart);
                return Ok(customMessages.MSG_18);
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
            return Ok(customMessages.MSG_19);
        }

        /*[HttpPut("{userId}")]
        public async Task<IActionResult> PutAsync(Guid userId, UpdateCartDto updateCartDto)
        {
            var existingCart = (await _cartRepository.GetAllAsync())
                .Where(c => c.UserId == userId)
                .FirstOrDefault();
            if (updateCartDto.ListProductInCart == null)
            {
                return BadRequest();
            }
            if (existingCart != null)
            {
                existingCart.ListProductInCart = updateCartDto.ListProductInCart;
                existingCart.UpdatedDate = DateTime.UtcNow;

                await _cartRepository.UpdateAsync(existingCart);
                return Ok(existingCart);
            }
            return NotFound();
        }*/

        [HttpPut("PutOneItem{userId}")]
        public async Task<IActionResult> PutOneItem(Guid userId, ProductInCart productInCart)
        {
            var existingCart = (await _cartRepository.GetAllAsync())
                .Where(c => c.UserId == userId)
                .FirstOrDefault();
            if (productInCart == null)
            {
                return BadRequest(customMessages.MSG_02);
            }
            if (existingCart != null)
            {
                foreach(var item in existingCart.ListProductInCart)
                {
                    if(item.ProductId == productInCart.ProductId)
                    {
                        if (productInCart.Quantity == 0)
                        {
                            existingCart.ListProductInCart.Remove(item);
                            break;
                        }
                        item.Quantity = productInCart.Quantity;
                    }
                    else
                    {
                        existingCart.ListProductInCart.Add(productInCart);
                    }
                }
                await _cartRepository.UpdateAsync(existingCart);
                return Ok(customMessages.MSG_18);
            }
            return NotFound(customMessages.MSG_01);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAnItem([FromQuery] Guid cartId,[FromQuery] Guid itemId)
        {
            var cart = await _cartRepository.GetAsync(cartId);
            if (cart != null)
            {
                foreach(var product in cart.ListProductInCart)
                {
                    if(product.ProductId == itemId)
                    {
                        cart.ListProductInCart.Remove(product);
                    }
                }
                await _cartRepository.UpdateAsync(cart);
                return Ok(customMessages.MSG_19);
            }
            return NotFound(customMessages.MSG_01);
        }

    }
}
