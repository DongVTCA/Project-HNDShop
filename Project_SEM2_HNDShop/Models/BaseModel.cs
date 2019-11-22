using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_SEM2_HNDShop.Models
{
    public class BaseModel
    {
        [Required]
        public int Id { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
