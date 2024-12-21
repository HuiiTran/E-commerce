using MassTransit;
using UserContract;
using ServicesCommon;
using ReviewsApi.Entities;

namespace ReviewsApi.Consumers.UserConsumer
{
    public class CreateUser : IConsumer<UserCreate>
    {
        private readonly IRepository<User> _userRepository;

        public CreateUser(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<UserCreate> consumeContext)
        {
            var message = consumeContext.Message;
            var user = await _userRepository.GetAsync(message.Id);
            if (user != null)
            {
                return;
            }
            user = new User
            {
                Id = message.Id,
                UserName = message.UserName,
                BoughtProducts = message.BoughtProducts,
            };


            await _userRepository.CreateAsync(user);
        }
    }
}
