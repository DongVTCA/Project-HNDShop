using System.ComponentModel.DataAnnotations;

namespace Project_SEM2_HNDShop.Models
{
    public class Orderdetail : BaseModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}