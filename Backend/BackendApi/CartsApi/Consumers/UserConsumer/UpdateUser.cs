using CartsApi.Entities;
using MassTransit;
using ServicesCommon;
using UserContract;

namespace CartsApi.Consumers.UserConsumer
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
            if (user == null)
            {
                user = new User
                {
                    Id = message.Id,
                    userName = message.UserName
                };
                await _userRepository.CreateAsync(user);
            }
            else
            {
                user.userName = message.UserName;

                await _userRepository.UpdateAsync(user);
            }
        }
    }
}
