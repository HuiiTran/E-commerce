using ProductApi.Entities;
using MassTransit;
using ServicesCommon;
using StaffContract;

namespace ProductApi.Consumers.StaffConsumer
{
    public class CreateStaff : IConsumer<StaffCreate>
    {
        private readonly IRepository<User> _UserRepository;

        public CreateStaff(IRepository<User> UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public async Task Consume(ConsumeContext<StaffCreate> consumeContext)
        {
            var message = consumeContext.Message;

            var staff = await _UserRepository.GetAsync(message.Id);
            if (staff != null)
            {
                return;
            }
            staff = new User 
            { 
                Id = message.Id,
                Name = message.UserName,
            };
            await _UserRepository.CreateAsync(staff);
        }
    }
}
