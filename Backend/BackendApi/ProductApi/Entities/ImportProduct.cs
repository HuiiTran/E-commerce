namespace ProductApi.Entities
{
    public class ImportProduct
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
