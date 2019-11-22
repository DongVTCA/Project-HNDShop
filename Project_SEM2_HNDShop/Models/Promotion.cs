using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_SEM2_HNDShop.Models
{
    public class Promotion : BaseModel
    {
        public string PromoTitle { get; set; }

        [Range(0, 100)]
        public int DiscountPercent { get; set; }
        public string PromoDesc { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? EndDate { get; set; }
        public int PromoStatus { get; set; }
    }
}
