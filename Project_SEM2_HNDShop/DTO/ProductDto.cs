using Project_SEM2_HNDShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_SEM2_HNDShop.DTO
{
    public class ProductDto
    {
        public Product product { get; set; }
        public string SubBrandName { get; set; }
        public string BrandName { get; set; }
        public string CateType { get; set; }
        public int PromoDiscountPercent { get; set; }
        public SubBrand subBrand { get; set; }
        public Brand brand { get; set; }
        public Category category { get; set; }
        public Promotion promotion { get; set; }
    }
}
