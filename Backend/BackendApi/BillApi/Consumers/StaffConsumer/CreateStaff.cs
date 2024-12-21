using BillApi.Entities;
using MassTransit;
using ServicesCommon;
using StaffContract;

namespace BillApi.Consumers.StaffConsumer
{
    public class CreateStaff : IConsumer<StaffCreate>
    {
        private readonly IRepository<Staff> _staffRepository;

        public CreateStaff(IRepository<Staff> staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task Consume(ConsumeContext<StaffCreate> consumeContext)
        {
            var message = consumeContext.Message;

            var staff = await _staffRepository.GetAsync(message.Id);

            if (staff == null)
            {
                return;
            }
            staff = new Staff
            {
                Id = message.Id,
                Name = message.UserName,
                Role = message.Role,
            };
            await _staffRepository.CreateAsync(staff);
        }
    }
}
