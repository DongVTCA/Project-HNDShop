using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_SEM2_HNDShop.Data;
using Project_SEM2_HNDShop.DTO;
using Project_SEM2_HNDShop.Models;
using Project_SEM2_HNDShop.Services;

namespace Project_SEM2_HNDShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public AdminController(ApplicationContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult ProductAdmin()
        {
            GetListNav();
            return View(new ProductUploadDto());
        }
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> ProductAdmin([Bind("ProCode,ProName,SubBrandId,CateId,PromoId,Quantity,Size,Color,OriginPrice,ProDesc,MyImage")] ProductUploadDto productUploadDto)
        {
            var filename = productUploadDto.MyImage.FileName;
            string sourcepath = _hostingEnvironment.WebRootPath;
            string uploadpath = "lib\\Uploads";
            var pathsource = Path.Combine(sourcepath, uploadpath);
            string path = Path.Combine(pathsource, filename);
            string savepathdb = Path.Combine(uploadpath, filename);
            if (System.IO.File.Exists(path) == true)
            {
                return View();
            }
            else
            {
                productUploadDto.ProImage = savepathdb;
                productUploadDto.MyImage.CopyTo(new FileStream(path, FileMode.OpenOrCreate));
                var product = MappingUpload(productUploadDto);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }


        }
        private Product MappingUpload(ProductUploadDto input)
        {
            Product output = new Product();
            output.CateId = input.CateId;
            output.Color = input.Color;
            output.CreateTime = DateTime.Now;
            output.OriginPrice = input.OriginPrice;
            output.ProCode = input.ProCode;
            output.ProDesc = input.ProDesc;
            output.ProImage = input.ProImage;
            output.PromoId = input.PromoId;
            output.ProName = input.ProName;
            output.Quantity = input.Quantity;
            output.SellPrice = input.OriginPrice + (input.OriginPrice / 100 * 5);
            output.Size = input.Size;
            output.StatusCode = input.StatusCode;
            output.SubBrandId = input.SubBrandId;

            return output;
        }
        public void GetListNav()
        {
            ViewData["subbrand"] = _context.SubBrands.ToList();
            ViewData["category"] = _context.Categories.ToList();
            ViewData["promotion"] = _context.Promotions.ToList();
        }
    }
}