using Microsoft.AspNetCore.Http;
using Project_SEM2_HNDShop.Models;

namespace Project_SEM2_HNDShop.DTO
{
    public class ProductUploadDto : Product
    {
        //public Product product {get;set;}
        public IFormFile MyImage { set; get; }
    }
}