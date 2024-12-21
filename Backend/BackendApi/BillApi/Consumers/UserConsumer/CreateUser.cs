using BillApi.Entities;
using MassTransit;
using ServicesCommon;
using UserContract;

namespace BillApi.Consumers.UserConsumer
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
            if (user == null)
            {
                return;
            }
            user = new User { 
                Id = message.Id,
                Name = message.FullName,
                Role = message.Role,
            };


            await _userRepository.CreateAsync(user);
        }
    }
}
