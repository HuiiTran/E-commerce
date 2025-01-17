using AdminsApi.Dtos;
using AdminsApi.Entities;
using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc;
using ServicesCommon;
using AdminContract;

namespace AdminsApi.Controllers
{
    [ApiController]
    [Route("Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IRepository<Admin> _adminRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private CustomMessages customMessages = new CustomMessages();

        public AdminController(IRepository<Admin> adminRepository, IPublishEndpoint publishEndpoint)
        {
            _adminRepository = adminRepository;
            _publishEndpoint = publishEndpoint;
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
            await _publishEndpoint.Publish(new AdminCreate(admin.Id, admin.UserName, admin.Password, admin.Role));
            return Ok(customMessages.MSG_17);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateAdminDto updateAdminDto)
        {
            var existingAdmin = await _adminRepository.GetAsync(id);
            if (existingAdmin == null)
            {
                return NotFound(customMessages.MSG_01);
            }
            existingAdmin.Name = updateAdminDto.UserName;
            existingAdmin.PhoneNumber = updateAdminDto.PhoneNumber;
            existingAdmin.isDeleted = updateAdminDto.isDeleted;
            existingAdmin.LatestUpdatedDate = DateTime.UtcNow;

            await _adminRepository.UpdateAsync(existingAdmin);

            await _publishEndpoint.Publish(new AdminUpdate(existingAdmin.Id, existingAdmin.UserName, existingAdmin.Password, existingAdmin.Role));

            return Ok(customMessages.MSG_18);
        }

        [HttpPut("Password/{id}")]
        public async Task<IActionResult> PutPasswordAsync(Guid id, [FromForm] string oldPassword, [FromQuery]string newPassword)
        {
            var existingAdmin = await _adminRepository.GetAsync(id);
            if (existingAdmin == null)
            {
                return NotFound(customMessages.MSG_01);
            }
            if(existingAdmin.Password != oldPassword)
            {
                return BadRequest(customMessages.MSG_04);
            }
            existingAdmin.Password = newPassword;
            existingAdmin.LatestUpdatedDate = DateTime.UtcNow;

            await _adminRepository.UpdateAsync(existingAdmin);
            return Ok(customMessages.MSG_18);
        }
    }
}
