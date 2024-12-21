using AdminContract;
using AuthApi.Entities;
using MassTransit;
using ServicesCommon;

namespace AuthApi.Consumers.AdminConsumer
{
    public class CreateAdmin : IConsumer<AdminCreate>
    {
        private readonly IRepository<AllUser> _allUserRepository;

        public CreateAdmin(IRepository<AllUser> allUserRepository) 
        { 
            _allUserRepository = allUserRepository;
        }

        public async Task Consume(ConsumeContext<AdminCreate> consumeContext)
        {
            var message = consumeContext.Message;

            var admin = await _allUserRepository.GetAsync(message.Id);

            if (admin != null)
            {
                return;
            }
            admin = new AllUser
            {
                Id = message.Id,
                UserName = message.UserName,
                Password = message.Password,
                Role = message.Role,
            };

            await _allUserRepository.CreateAsync(admin);
        }
    }
}
