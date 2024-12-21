using BillApi.Entities;
using MassTransit;
using ServicesCommon;
using UserContract;

namespace BillApi.Consumers.UserConsumer
{
    public class UpdateUser : IConsumer<UserUpdate>
    {
        private readonly IRepository<User> _userRepository;

        public UpdateUser(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<UserUpdate> consumeContext)
        {
            var message = consumeContext.Message;

            var user = await _userRepository.GetAsync(message.Id);
            if(user == null)
            {
                user = new User
                {
                    Id = message.Id,
                    Name = message.FullName,
                    Role = message.Role,
                };
                await _userRepository.CreateAsync(user);
            }
            else
            {

                user.Name = message.FullName;
                user.Role = message.Role;

                await _userRepository.UpdateAsync(user);
            }
        }
    }
}
