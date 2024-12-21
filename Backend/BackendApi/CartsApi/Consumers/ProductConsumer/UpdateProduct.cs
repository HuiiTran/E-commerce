using CartApi.Entities;
using MassTransit;
using ProductContract;
using ServicesCommon;

namespace CartsApi.Consumers.ProductConsumer
{
    public class UpdateProduct : IConsumer<ProductUpdate>
    {
        private readonly IRepository<Product> _productRepository;

        public UpdateProduct(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Consume(ConsumeContext<ProductUpdate> context)
        {
            var message = context.Message;
            var product = await _productRepository.GetAsync(message.id);
            if (product == null)
            {
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
            else
            {
                product.ProductName = message.ProductName;
                product.ProductPrice = message.ProductPrice;
                product.ProductImage = message.ProductImage;
                product.ProductQuantity = message.ProductQuantity;
                product.DiscountPrecentage = message.DiscountPrecentage;

                await _productRepository.UpdateAsync(product);
            }
        }
    }
}
