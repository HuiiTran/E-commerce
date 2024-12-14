using ServicesCommon;

namespace ProductApi.Entities
{
    public class ImportProductBill : IEntity
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public List<ImportProduct> ListImportProducts { get; set; } = new List<ImportProduct>();
        
        public Decimal Total { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }

    }
}
