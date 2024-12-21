using MassTransit;
using UserContract;
using ServicesCommon;
using ReviewsApi.Entities;
namespace ReviewsApi.Consumers.UserConsumer
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
                    UserName = message.UserName,
                    BoughtProducts = message.BoughtProducts,
                };
                await _userRepository.CreateAsync(user);
            }
            else
            {
                user.UserName = message.UserName;
                user.BoughtProducts = message.BoughtProducts;

                await _userRepository.UpdateAsync(user);
            }
        }
    }
}
