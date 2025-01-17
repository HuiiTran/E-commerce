using ProductApi.Entities;
using MassTransit;
using ServicesCommon;
using ProductContract;

namespace CartsApi.Consumers.ProductConsumer
{
    public class UpdateProductQuantity : IConsumer<UpdateQuantity>
    {
        private readonly IRepository<Product> _productRepository;

        public UpdateProductQuantity(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<UpdateQuantity> context)
        {
            var message = context.Message;
            var product = await _productRepository.GetAsync(message.id);
            if (product == null)
            {
                return;
            }
            else
            {
                product.ProductQuantity = message.quantity;

                await _productRepository.UpdateAsync(product);
            }
           
        }
    }
}
