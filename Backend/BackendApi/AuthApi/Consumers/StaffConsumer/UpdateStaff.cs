using AuthApi.Entities;
using MassTransit;
using ServicesCommon;
using StaffContract;

namespace AuthApi.Consumers.StaffConsumer
{
    public class UpdateStaff : IConsumer<StaffUpdate>
    {
        private readonly IRepository<AllUser> _userRepository;

        public UpdateStaff(IRepository<AllUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<StaffUpdate> consumeContext)
        {
            var message = consumeContext.Message;

            var staff = await _userRepository.GetAsync(message.Id);

            if (staff == null)
            {
                staff = new AllUser
                {
                    Id = message.Id,
                    UserName = message.UserName,
                    Password = message.Password,
                    Role = message.Role,
                };
                await _userRepository.CreateAsync(staff);
            }
            else
            {
                staff.UserName = message.UserName;
                staff.Password = message.Password;
                staff.Role = message.Role;

                await _userRepository.UpdateAsync(staff);
            }
        }
    }
}
