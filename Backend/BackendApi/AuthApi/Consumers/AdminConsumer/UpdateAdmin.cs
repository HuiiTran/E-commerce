using AdminContract;
using AuthApi.Entities;
using MassTransit;
using ServicesCommon;

namespace AuthApi.Consumers.AdminConsumer
{
    public class UpdateAdmin : IConsumer<AdminUpdate>
    {
        private readonly IRepository<AllUser> _userRepository;

        public UpdateAdmin(IRepository<AllUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<AdminUpdate> consumeContext)
        {
            var message = consumeContext.Message;

            var admin = await _userRepository.GetAsync(message.Id);

            if(admin == null)
            {
                admin = new AllUser
                {
                    Id = message.Id,
                    UserName = message.UserName,
                    Password = message.Password,
                    Role = message.Role,
                };
                await _userRepository.CreateAsync(admin);
            }
            else
            {
                admin.UserName = admin.UserName;
                admin.Password = admin.Password;
                admin.Role = admin.Role;

                await _userRepository.UpdateAsync(admin);
            }
        }
    }
}
