using Microsoft.AspNetCore.Mvc;
using ServicesCommon;
using StaffsApi.Dtos;
using StaffsApi.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace StaffsApi.Controllers
{
    [ApiController]
    [Route("Staff")]
    public class StaffController : ControllerBase
    {
        private readonly IRepository<Staff> _staffRepository;
        private const int pageSize = 10;
        public StaffController(IRepository<Staff> staffRepository)
        {
            _staffRepository = staffRepository;
        }


        [HttpGet("PagesAmount")]
        public async Task<ActionResult<int>> GetPagesAmount()
        {
            int pagesAmount = 1;
            var totalStaff = (await _staffRepository.GetAllAsync())
                .Count();
            if (totalStaff > pageSize)
            {
                pagesAmount = (totalStaff % pageSize == 0) ? (totalStaff % pageSize) : (totalStaff % pageSize + 1);
            }
            if (pagesAmount <= 0)
            {
                pagesAmount = 1;
            }
            return Ok(pagesAmount);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffDto>>> GetAsync([FromQuery] int pageNumber = 1)
        {
            if (pageNumber < 1) { return BadRequest(); }

            var staffs = (await _staffRepository.GetAllAsync())
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(staff => staff.AsDto());
            return Ok(staffs);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StaffDto>> GetByIdAsync(Guid id)
        {
            var staff = await _staffRepository.GetAsync(id);

            if (staff == null) { return NotFound(); }
            return staff.AsDto();
        }
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<StaffDto>>> SearchAsync([FromQuery] string searchString)
        {
            var staffs = (await _staffRepository.GetAllAsync())
                .Where(s => s.Name.Contains(searchString) || s.PhoneNumber.Contains(searchString) || s.Email.Contains(searchString))
                .OrderBy(s => s.Id)
                .Select(staff => staff.AsDto());

            if (staffs == null) { return NotFound(); }
            return Ok(staffs);
        }

        [HttpPost]
        public async Task<ActionResult<StaffDto>> PostAsync([FromForm] CreateStaffDto createStaffDto)
        {
            var staff = new Staff
            {
                UserName = createStaffDto.UserName,
                Password = createStaffDto.Password,
                Email = createStaffDto.Email,
                Name = createStaffDto.Name,
                PhoneNumber = createStaffDto.Phone
            };
            if(createStaffDto.Image == null)
            {
                staff.Image = "";
            }
            else
            {
                MemoryStream memoryStream = new MemoryStream();
                createStaffDto.Image.OpenReadStream().CopyTo(memoryStream);
                staff.Image = Convert.ToBase64String(memoryStream.ToArray());
            }

            await _staffRepository.CreateAsync(staff);

            return Ok(staff);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromForm] UpdateStaffDto updateStaffDto)
        {
            var existingStaff = await _staffRepository.GetAsync(id);

            var existingImage = existingStaff.Image;
            if( existingStaff == null ) { return NotFound(); }
            existingStaff.UserName = updateStaffDto.UserName;
            existingStaff.Password = updateStaffDto.Password;
            existingStaff.Email = updateStaffDto.Email;
            existingStaff.Name = updateStaffDto.Name;
            existingStaff.PhoneNumber = updateStaffDto.Phone;
            existingStaff.isDeleted = updateStaffDto.isDeleted;

            if(updateStaffDto.Image != null )
            {
                MemoryStream memoryStream = new MemoryStream();
                updateStaffDto.Image.OpenReadStream().CopyTo(memoryStream);
                existingStaff.Image = Convert.ToBase64String(memoryStream.ToArray());
            }
            else
            {
                existingStaff.Image = existingImage;
            }
            await _staffRepository.UpdateAsync(existingStaff);

            return Ok();
        }
        [HttpPut("ChangePassword{id}")]
        public async Task<IActionResult> ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            var staff = await _staffRepository.GetAsync(id);
            if(staff == null) { return NotFound();}
            if(staff.Password != oldPassword)
            {
                return BadRequest();
            }
            staff.Password = newPassword;

            await _staffRepository.UpdateAsync(staff);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var staff = await _staffRepository.GetAsync(id);
            if(staff == null) { return NotFound();}

            staff.isDeleted = true;

            await _staffRepository.UpdateAsync(staff);

            return Ok();
        }
    }
}
