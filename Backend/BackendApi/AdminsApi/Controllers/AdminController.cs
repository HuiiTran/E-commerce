using AdminsApi.Dtos;
using AdminsApi.Entities;
using Microsoft.AspNetCore.Mvc;
using ServicesCommon;

namespace AdminsApi.Controllers
{
    [ApiController]
    [Route("Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IRepository<Admin> _adminRepository;

        public AdminController(IRepository<Admin> adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAsync()
        {
            var admins = (await _adminRepository.GetAllAsync())
                .Select(admin => admin.AsDto());
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDto>> GetByIdAsync(Guid id)
        {
            var admin = await _adminRepository.GetAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPost]
        public async Task<ActionResult<AdminDto>> PostAsync(CreateAdminDto createAdminDto)
        {
            
            var admin = new Admin
            {
                UserName = createAdminDto.UserName,
                Password = createAdminDto.Password,
                Email = createAdminDto.Email,
                Name = createAdminDto.Name,
                PhoneNumber = createAdminDto.PhoneNumber,
                isDeleted = false,
                CreatedDate = DateTime.UtcNow,
                LatestUpdatedDate = DateTime.UtcNow,

            };
            await _adminRepository.CreateAsync(admin);
            return Ok(admin);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateAdminDto updateAdminDto)
        {
            var existingAdmin = await _adminRepository.GetAsync(id);
            if (existingAdmin == null)
            {
                return NotFound();
            }
            existingAdmin.Name = updateAdminDto.UserName;
            existingAdmin.PhoneNumber = updateAdminDto.PhoneNumber;
            existingAdmin.isDeleted = updateAdminDto.isDeleted;
            existingAdmin.LatestUpdatedDate = DateTime.UtcNow;

            await _adminRepository.UpdateAsync(existingAdmin);
            return Ok("Update success");
        }

        [HttpPut("Password/{id}")]
        public async Task<IActionResult> PutPasswordAsync(Guid id, [FromForm] string oldPassword, [FromQuery]string newPassword)
        {
            var existingAdmin = await _adminRepository.GetAsync(id);
            if (existingAdmin == null)
            {
                return NotFound();
            }
            if(existingAdmin.Password != oldPassword)
            {
                return BadRequest();
            }
            existingAdmin.Password = newPassword;
            existingAdmin.LatestUpdatedDate = DateTime.UtcNow;

            await _adminRepository.UpdateAsync(existingAdmin);
            return Ok(existingAdmin);
        }
    }
}
