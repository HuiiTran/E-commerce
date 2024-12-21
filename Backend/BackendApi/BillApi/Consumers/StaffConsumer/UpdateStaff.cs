using BillApi.Entities;
using MassTransit;
using ServicesCommon;
using StaffContract;

namespace BillApi.Consumers.StaffConsumer
{
    public class UpdateStaff : IConsumer<StaffUpdate>
    {
        private readonly IRepository<Staff> _staffRepository;

        public UpdateStaff(IRepository<Staff> staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task Consume(ConsumeContext<StaffUpdate> consumeContext)
        {
            var message = consumeContext.Message;

            var staff = await _staffRepository.GetAsync(message.Id);

            if (staff == null)
            {
                staff = new Staff
                {
                    Id = message.Id,
                    Name = message.UserName,
                    Role = message.Role,
                };
                 await _staffRepository.CreateAsync(staff);
            }
            else
            {
                staff.Name = message.UserName;
                staff.Role = message.Role;

                await _staffRepository.UpdateAsync(staff);
            }
        }
    }
}
