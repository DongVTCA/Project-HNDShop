using System.ComponentModel.DataAnnotations;

namespace Project_SEM2_HNDShop.Models
{
    public class Feedback : BaseModel
    {

        public int UserId { get; set; }

        public int ProId { get; set; }
        public string title { get; set; }
        public string Comment { get; set; }
        public int? Rate { get; set; }

    }
}