using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductContract
{
    public record ProductCreate(
        Guid id, 
        string ProductName, 
        decimal ProductPrice,
        int DiscountPrecentage, 
        int ProductQuantity, 
        string ProductImage );
    public record ProductUpdate(
        Guid id,
        string ProductName,
        decimal ProductPrice,
        int DiscountPrecentage,
        int ProductQuantity,
        string ProductImage);
    public record ProductDelete(
        Guid id);

    public record UpdateQuantity(
        int quantity
        );
}
