using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using ServicesCommon;
using UserContract;
using UsersApi.Dtos;
using UsersApi.Entities;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("User")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private const int pageSize = 10;
        public UsersController(IRepository<User> userRepository, IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAsync([FromQuery]int pageNumber = 1)
        {
            CustomMessages customMessages = new CustomMessages();
            if (pageNumber < 1) { return BadRequest(customMessages.MSG_23); }

            var users = (await _userRepository.GetAllAsync())
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(user => user.AsDto());
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetByIdAsync(Guid id)
        {
            CustomMessages customMessages = new CustomMessages();
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                return NotFound(customMessages.MSG_01);
            }
            return user.AsDto();
        }
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<UserDto>>> SearchAsync([FromQuery]string searchString, [FromQuery]int pageNumber)
        {
            CustomMessages customMessages = new CustomMessages();
            var users = (await _userRepository.GetAllAsync())
                .Where(u => u.FullName.Contains(searchString) || u.Email.Contains(searchString) || u.PhoneNumber.Contains(searchString))
                .OrderBy(u => u.Id)
                .Select(user => user.AsDto());
            if (users == null) return NotFound(customMessages.MSG_01);
            return Ok(users);
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostAsync(CreateUserDto createUserDto)
        {
            CustomMessages customMessages = new CustomMessages();
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            var users = (await _userRepository.GetAllAsync())
                .Where(u => u.UserName == createUserDto.UserName);
            if(users != null)
            {
                return BadRequest(customMessages.MSG_24);
            }
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

            await _publishEndpoint.Publish(new UserCreate(  user.Id, 
                                                            user.FullName, 
                                                            user.UserName, 
                                                            user.Password,
                                                            user.BoughtProducts,
                                                            user.Role));
            return Ok(customMessages.MSG_17);
        }
        [HttpPut("ConfirmUser")]
        public async Task<IActionResult> ConfirmUser([FromQuery] Guid userId, [FromQuery] int ConfirmCode)
        {
            CustomMessages customMessages = new CustomMessages();
            var user = await _userRepository.GetAsync(userId);
            if (user == null) { return NotFound(customMessages.MSG_01); }
            if (ConfirmCode == user.ConfirmedCode)
            {
                user.IsEmailConfirmed = true;
                await _userRepository.UpdateAsync(user);
            }
            else
            {
                return BadRequest(customMessages.MSG_16);
            }
            return Ok(customMessages.MSG_18);
        }
        [HttpPut("UpdateEmail")]
        public async Task<IActionResult> UpdateEmail([FromQuery]string newEmail, [FromQuery] Guid userId)
        {
            CustomMessages customMessages = new CustomMessages();
            var user = await _userRepository.GetAsync(userId);
            if (user == null) { return NotFound(customMessages.MSG_01); }
            user.Email = newEmail;

            await _userRepository.UpdateAsync(user);
            return Ok(customMessages.MSG_18);
        }
        [HttpPut("ChangePassword{id}")]
        public async Task<IActionResult> ChangePassword(Guid id,ChangePasswordDto changePasswordDto)
        {
            CustomMessages customMessages = new CustomMessages();
            var user = await _userRepository.GetAsync(id);
            if(user == null) { return NotFound(customMessages.MSG_01); }
            if (user.Password == changePasswordDto.OldPassword)
            {
                user.Password = changePasswordDto.newPassword;
                await _userRepository.UpdateAsync(user);
                await _publishEndpoint.Publish(new UserCreate(user.Id,
                                                            user.FullName,
                                                            user.UserName,
                                                            user.Password,
                                                            user.BoughtProducts,
                                                            user.Role));
                return Ok(customMessages.MSG_18);
            }
            return BadRequest(customMessages.MSG_21);
        }
        
        [HttpPut("AddMoreBoughtProduct")]
        public async Task<IActionResult> AddMoreBoughtProduct([FromQuery] Guid userId, [FromQuery] Guid productId)
        {
            CustomMessages customMessages = new CustomMessages();
            var user = await _userRepository.GetAsync(userId);
            if (user == null) { return NotFound(customMessages.MSG_01); }
            foreach (var itemId in user.BoughtProducts)
            {
                if (itemId != productId)
                {
                    user.BoughtProducts.Add(productId);
                }
            }

            await _userRepository.UpdateAsync(user);
            await _publishEndpoint.Publish(new UserCreate(user.Id,
                                                            user.FullName,
                                                            user.UserName,
                                                            user.Password,
                                                            user.BoughtProducts,
                                                            user.Role));
            return Ok(customMessages.MSG_17);
        }
        [HttpGet("isBoughtProduct")]
        public async Task<ActionResult> CheckBought([FromQuery]Guid userId, [FromQuery]Guid productId)
        {
            CustomMessages customMessages = new CustomMessages();
            var user = await _userRepository.GetAsync(userId);
            if (user == null) return NotFound(customMessages.MSG_01);
            foreach(var product in user.BoughtProducts)
            {
                if(product != productId)
                {
                    return BadRequest(customMessages.MSG_02);
                }
            }
            return Ok();
        }

    }
}
