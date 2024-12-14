using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using ServicesCommon;
using UsersApi.Dtos;
using UsersApi.Entities;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("User")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAsync()
        {
            var users = (await _userRepository.GetAllAsync())
                .Select(user => user.AsDto());
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostAsync(CreateUserDto createUserDto)
        {
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            var user = new User
            {
                UserName = createUserDto.UserName,
                Password = createUserDto.Password,
                Email = createUserDto.Email,
                IsEmailConfirmed = false,
                ConfirmedCode = randomNumber,
                FullName = createUserDto.FullName,
                PhoneNumber = createUserDto.PhoneNumber,
                Address = createUserDto.Address,
                BoughtProducts = new List<Guid>(),
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,
            };
            await _userRepository.CreateAsync(user);
            return Ok(user);
        }
        [HttpPut("ConfirmUser")]
        public async Task<IActionResult> ConfirmUser([FromQuery] Guid userId, [FromQuery] int ConfirmCode)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) { return NotFound(); }
            if (ConfirmCode == user.ConfirmedCode)
            {
                user.IsEmailConfirmed = true;
                await _userRepository.UpdateAsync(user);
            }
            return Ok(user);
        }
        [HttpPut("UpdateEmail")]
        public async Task<IActionResult> UpdateEmail([FromQuery]string newEmail, [FromQuery] Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) { return NotFound(); }
            user.Email = newEmail;

            await _userRepository.UpdateAsync(user);
            return Ok(user);
        }
        [HttpPut("ChangePassword{id}")]
        public async Task<IActionResult> ChangePassword(Guid id,ChangePasswordDto changePasswordDto)
        {

            var user = await _userRepository.GetAsync(id);
            if(user == null) { return NotFound(); }
            if (user.Password == changePasswordDto.OldPassword)
            {
                user.Password = changePasswordDto.newPassword;
                await _userRepository.UpdateAsync(user);

                return Ok();
            }
            return BadRequest();
        }
        [HttpPut("AddMoreBoughtProduct")]
        public async Task<IActionResult> AddMoreBoughtProduct([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) { return NotFound(); }
            foreach (var itemId in user.BoughtProducts)
            {
                if (itemId != productId)
                {
                    user.BoughtProducts.Add(productId);
                }
            }

            await _userRepository.UpdateAsync(user);
            return Ok(user);
        }
        [HttpGet("isBoughtProduct")]
        public async Task<ActionResult> CheckBought([FromQuery]Guid userId, [FromQuery]Guid productId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null) return NotFound();
            foreach(var product in user.BoughtProducts)
            {
                if(product != productId)
                {
                    return BadRequest();
                }
            }

            return Ok();
        }

    }
}
