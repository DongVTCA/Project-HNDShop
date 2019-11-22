using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_SEM2_HNDShop.Models
{
    public class SubBrand : BaseModel
    {
        public int BrandId { get; set; }
        public string SubBrandName { get; set; }
        public string SubBrandDesc { get; set; }
    }
}
