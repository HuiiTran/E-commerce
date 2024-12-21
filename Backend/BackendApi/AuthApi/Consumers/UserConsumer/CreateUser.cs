﻿using AuthApi.Entities;
using MassTransit;
using ServicesCommon;
using UserContract;

namespace AuthApi.Consumers.UserConsumer
{
    public class CreateUser : IConsumer<UserCreate>
    {
        private readonly IRepository<AllUser> _allUserRepository;

        public CreateUser(IRepository<AllUser> allUserRepository)
        {
            _allUserRepository = allUserRepository;
        }

        public async Task Consume(ConsumeContext<UserCreate> consumeContext)
        {
            var message = consumeContext.Message;

            var user = await _allUserRepository.GetAsync(message.Id);

            if (user != null)
            {
                return;
            }

            user = new AllUser
            {
                Id = message.Id,
                UserName = message.UserName,
                Password = message.Password,
                Role = message.Role,
            };

            await _allUserRepository.CreateAsync(user);
        }
    }
}
