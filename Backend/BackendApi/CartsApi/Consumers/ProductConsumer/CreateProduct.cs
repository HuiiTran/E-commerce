using CartApi.Entities;
using MassTransit;
using ProductContract;
using ServicesCommon;

namespace CartsApi.Consumers.ProductConsumer
{
    public class CreateProduct : IConsumer<ProductCreate>
    {
        private readonly IRepository<Product> _productRepository;

        public CreateProduct(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<ProductCreate> consumeContext)
        {
            var message = consumeContext.Message;

            var product = await _productRepository.GetAsync(message.id);

            if (product != null)
            {
                return;
            }
            product = new Product
            {
                Id = message.id,
                ProductName = message.ProductName,
                ProductPrice = message.ProductPrice,
                ProductImage = message.ProductImage,
                ProductQuantity = message.ProductQuantity,
                DiscountPrecentage = message.DiscountPrecentage
            };
            await _productRepository.CreateAsync(product);
        }
    }
}
