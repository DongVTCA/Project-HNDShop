namespace Project_SEM2_HNDShop.Models
{
    public class Configuation : BaseModel
    {
        public int UserId { get; set; }
        public string Location { get; set; }
        public string EmailSupport { get; set; }
        public string PhoneSupport { get; set; }
    }
}