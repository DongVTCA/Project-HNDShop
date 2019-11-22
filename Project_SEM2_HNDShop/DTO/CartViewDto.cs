using Project_SEM2_HNDShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_SEM2_HNDShop.DTO
{
    public class CartViewDto
    {
        public int Id { get; set; }
        public Product product { get; set; }
        public int DiscountPercent { get; set; }
        public int Quantity { get; set; }

    }
}
