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
    public class CartController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly Repository _repository;

        public CartController(ApplicationContext context)
        {
            _context = context;
            _repository = new Repository(_context);
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("userId") == null)
            {
                ViewBag.message = "You must login to view cart";
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var userId = HttpContext.Session.GetInt32("userId");
                var listcart = _repository.GetListCartByUserId((int)userId);
                return View(listcart);
            }
        }


        public IActionResult AddToCart(int id, int quantity)
        {

            if (HttpContext.Session.GetInt32("userId") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var cart = new Cart();
                cart.ProductId = id;
                cart.Quantity = quantity;
                cart.UserId = (int)HttpContext.Session.GetInt32("userId");
                var cartItem = _context.Carts.FirstOrDefault(c => c.ProductId == id && c.UserId == cart.UserId);
                if (cartItem != null)
                {
                    cartItem.Quantity += cart.Quantity;
                    _context.Carts.Update(cartItem);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                _context.Add(cart);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult UpdateCountCart(int id, int quantity)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return BadRequest();
            }
            else
            {
                var cartItem = _context.Carts.FirstOrDefault(c => c.Id == id && c.UserId == userId);
                cartItem.Quantity = quantity;
                _context.Update(cartItem);
                _context.SaveChanges();
                return View(nameof(Index));
            }
        }

        public IActionResult DeleteCartItem(int id)
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null)
            {
                return BadRequest();
            }
            else
            {
                var cartItem = _context.Carts.FirstOrDefault(c => c.Id == id && c.UserId == userId);
                if (cartItem != null)
                {
                    _context.Carts.Remove(cartItem);
                    _context.SaveChanges();
                    return View(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}