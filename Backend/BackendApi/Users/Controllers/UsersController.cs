using Microsoft.AspNetCore.Mvc;
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
            var user = new User
            {
                UserName = createUserDto.UserName,
                Password = createUserDto.Password,
                Email = createUserDto.Email,
                IsEmailConfirmed = false,
                FullName = createUserDto.FullName,
                PhoneNumber = createUserDto.PhoneNumber,
                Address = createUserDto.Address
            };
        }

    }
}
