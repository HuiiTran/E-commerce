using AuthApi.Entities;
using MassTransit;
using ServicesCommon;
using UserContract;

namespace AuthApi.Consumers.UserConsumer
{
    public class UpdateUser : IConsumer<UserUpdate>
    {
        private readonly IRepository<AllUser> _allUserRepository;

        public UpdateUser(IRepository<AllUser> allUserRepository)
        {
            _allUserRepository = allUserRepository;
        }
        
        public async Task Consume(ConsumeContext<UserUpdate> consumeContext)
        {
            var message = consumeContext.Message;

            var user = await _allUserRepository.GetAsync(message.Id);
            if (user == null)
            {
                user = new AllUser
                {
                    Id = message.Id,
                    UserName = message.UserName,
                    Password = message.Password,
                    Role = message.Role,
                };
                await _allUserRepository.CreateAsync(user);
            }
            else
            {
                user.UserName = message.UserName;
                user.Password = message.Password;
                user.Role = message.Role;

                await _allUserRepository.UpdateAsync(user);
            }
        }
    }
}
