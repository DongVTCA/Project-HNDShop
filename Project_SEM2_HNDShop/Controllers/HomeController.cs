using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_SEM2_HNDShop.Data;
using Project_SEM2_HNDShop.DTO;
using Project_SEM2_HNDShop.Models;
using Project_SEM2_HNDShop.Services;
namespace Project_SEM2_HNDShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;
        private readonly Repository _repository;
        private MD5 md5;
        private static EnCryptography _encrypt;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
            _repository = new Repository(_context);
        }

        public IActionResult Index()
        {
            ViewBag.sessionCart = HttpContext.Session.GetInt32("cartId");
            if (HttpContext.Session.GetString("userName") != null && HttpContext.Session.GetInt32("userId") != null)
            {
                var useridsession = HttpContext.Session.GetInt32("userId");
                ViewBag.sessionName = HttpContext.Session.GetString("userName");
                ViewBag.Itemcart = _repository.GetListItemCartByUserIdOrderByDesc((int)useridsession);
            }
            else
            {
                ViewBag.session = null;
            }
            var listItemViewIndex = _repository.GetListProduct();
            GetListNav();
            return View(listItemViewIndex);
        }

        public void GetListNav()
        {
            ViewData["subbrand"] = _context.SubBrands.ToList();
            ViewData["category"] = _context.Categories.ToList();
            ViewData["promotion"] = _context.Promotions.ToList();
        }
        public IActionResult RemoveSessionCart()
        {
            if (HttpContext.Session.GetInt32("userId") != null && HttpContext.Session.GetInt32("cartId") != null)
            {
                HttpContext.Session.Remove("cartId");
                return RedirectToAction("Index", "Home");
            }
            return BadRequest();
        }
        public IActionResult Logout()
        {
            GetListNav();
            if (HttpContext.Session.GetString("userName") != null && HttpContext.Session.GetInt32("userId") != null)
            {
                HttpContext.Session.Remove("userName");
                HttpContext.Session.Remove("userId");
                HttpContext.Session.Clear();
                return Redirect("/");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult DetailProduct(int id)
        {
            GetListNav();
            var productdetail = _repository.GetDetailProduct(id);
            return View(productdetail);
        }

        public IActionResult Register()
        {
            GetListNav();
            return View();
        }

        // public IActionResult Product () {
        //     GetListNav ();
        //     var listproductdto = _repository.GetListProduct ();
        //     return View (listproductdto);
        // }
        public IActionResult Login()
        {
            GetListNav();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            GetListNav();
            bool checkpass = false;
            var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (ModelState.IsValid)
            {
                using (md5 = MD5.Create())
                {
                    _encrypt = new EnCryptography();
                    checkpass = _encrypt.VerifyMd5Hash(md5, password, loginUser.UserPassword);
                }
                if (checkpass == true)
                {
                    HttpContext.Session.SetString("userName", loginUser.FirstName);
                    HttpContext.Session.SetInt32("userId", loginUser.Id);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Email,UserPassword,FirstName,LastName,Phone,Address")] User user)
        {
            GetListNav();
            string hashpass;
            if (ModelState.IsValid)
            {
                var checkRegister = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (checkRegister == null)
                {
                    using (md5 = MD5.Create())
                    {
                        _encrypt = new EnCryptography();
                        hashpass = _encrypt.GetMd5Hash(md5, user.UserPassword);
                    }
                    user.Active = 1;
                    user.Rank = 0;
                    user.RoleId = 1;
                    user.UserPassword = hashpass;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    ViewBag.TitleRegisterSuccessfully = "Dang ky thanh cong!";
                    return RedirectToAction(nameof(Login));
                }
            }
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public async Task<ActionResult> ChangePassword(string oldpassword, string password, string repassword)
        {
            string hashpass;
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return BadRequest();
            }
            else
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (oldpassword == user.UserPassword)
                {
                    if (password == repassword)
                    {
                        using (md5 = MD5.Create())
                        {
                            _encrypt = new EnCryptography();
                            hashpass = _encrypt.GetMd5Hash(md5, repassword);
                        }
                        user.UserPassword = hashpass;
                        _context.Users.Update(user);
                        await _context.SaveChangesAsync();
                        // return RedirectToAction ("Index", "Home");
                    }
                }

            }
            return RedirectToAction("Index", "Home");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}