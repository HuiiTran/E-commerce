using ProductApi.Entities;
using MassTransit;
using ServicesCommon;
using StaffContract;

namespace ProductApi.Consumers.StaffConsumer
{
    public class UpdateStaff : IConsumer<StaffUpdate>
    {
        private readonly IRepository<User> _userRepository;

        public UpdateStaff(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<StaffUpdate> consumeContext)
        {
            var message = consumeContext.Message;

            var staff = await _userRepository.GetAsync(message.Id);

            if (staff == null)
            {
                staff = new User
                {
                    Id = message.Id,
                    Name = message.UserName,
                };
                await _userRepository.CreateAsync(staff);
            }
            else
            {
                staff.Name = message.UserName;

                await _userRepository.UpdateAsync(staff);
            }
        }
    }
}
