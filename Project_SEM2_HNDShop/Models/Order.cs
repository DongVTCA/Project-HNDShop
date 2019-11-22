using System.Collections.Generic;

namespace Project_SEM2_HNDShop.Models
{
    public class Order : BaseModel
    {
        public int UserId { get; set; }
        public string CusName { get; set; }
        public string CusPhone { get; set; }
        public string CusAddress { get; set; }

        public int StatusCode { get; set; }
        public List<Orderdetail> listOrderdetail { get; set; }
    }
}