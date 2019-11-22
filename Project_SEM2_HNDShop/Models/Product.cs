using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_SEM2_HNDShop.Models
{
    public class Product : BaseModel
    {
        public int PromoId { get; set; }
        public int SubBrandId { get; set; }
        public int CateId { get; set; }
        public string ProCode { get; set; }
        public string ProName { get; set; }
        public string ProImage { get; set; }
        public string ProDesc { get; set; }
        public int Quantity { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public double OriginPrice { get; set; }
        public double SellPrice { get; set; }
        public int StatusCode { get; set; }
    }
}
