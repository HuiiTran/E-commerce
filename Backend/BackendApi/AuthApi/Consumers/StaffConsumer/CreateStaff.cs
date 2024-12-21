using AuthApi.Entities;
using MassTransit;
using ServicesCommon;
using StaffContract;

namespace AuthApi.Consumers.StaffConsumer
{
    public class CreateStaff : IConsumer<StaffCreate>
    {
        private readonly IRepository<AllUser> _allUserRepository;

        public CreateStaff(IRepository<AllUser> allUserRepository)
        {
            _allUserRepository = allUserRepository;
        }

        public async Task Consume(ConsumeContext<StaffCreate> consumeContext)
        {
            var message = consumeContext.Message;

            var staff = await _allUserRepository.GetAsync(message.Id);
            if (staff != null)
            {
                return;
            }
            staff = new AllUser 
            { 
                Id = message.Id,
                UserName = message.UserName,
                Password = message.Password,
                Role = message.Role,
            };
            await _allUserRepository.CreateAsync(staff);
        }
    }
}
