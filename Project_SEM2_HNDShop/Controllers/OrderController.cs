using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_SEM2_HNDShop.Data;
using Project_SEM2_HNDShop.DTO;
using Project_SEM2_HNDShop.Models;
using Project_SEM2_HNDShop.Services;

namespace Project_SEM2_HNDShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly Repository _repository;

        public OrderController(ApplicationContext context)
        {
            _context = context;
            _repository = new Repository(_context);
        }
        public IActionResult Index()
        {
            return View();
        }

        private Order InputOrder(int userId, int status, UserInfoOrderDto user)
        {
            Order order = new Order();
            order.UserId = userId;
            order.CreateTime = DateTime.Now;
            order.StatusCode = status;
            order.CusName = user.CusName != null ? user.CusName : null;
            order.CusPhone = user.CusPhone;
            order.CusAddress = user.CusAddress;
            return order;

        }
        private Orderdetail InputOrderDetail(int orderId, int productId, int quantity, double SellPrice, int DiscountPercent)
        {
            Orderdetail orderdetail = new Orderdetail();
            orderdetail.OrderId = orderId;
            orderdetail.ProductId = productId;
            orderdetail.Quantity = quantity;
            orderdetail.Price = (SellPrice / 100) * (100 - DiscountPercent);

            return orderdetail;
        }

        [HttpPost]
        public async Task<IActionResult> Buy([Bind("cusName,cusPhone,cusAddress")] UserInfoOrderDto infouser)
        {
            var userIdsession = HttpContext.Session.GetInt32("userId");
            var cart = _context.Carts.ToList().Where(c => c.UserId == userIdsession);
            var user = await _context.Users.FindAsync(userIdsession);
            Order order = new Order();
            order = InputOrder(user.Id, 1, infouser);
            //order.UserId = user.Id;
            //order.CreateTime = DateTime.Now;
            //order.StatusCode = 1;
            _context.Add(order);
            await _context.SaveChangesAsync();
            var lastorder = _repository.GetLastOrderByUserId((int)userIdsession);
            foreach (var item in cart)
            {
                Orderdetail orderdetail = new Orderdetail();
                var product = await _context.Products.FindAsync(item.ProductId);
                var promotion = await _context.Promotions.FindAsync(product.PromoId);

                if (product != null && product.StatusCode != 0 && product.Quantity >= item.Quantity)
                {
                    orderdetail = InputOrderDetail(lastorder.Id, item.ProductId, item.Quantity, product.SellPrice, promotion.DiscountPercent);
                    //orderdetail.Price = (product.SellPrice / 100) * (100 - promotion.DiscountPercent);
                    //orderdetail.Quantity = item.Quantity;
                    //orderdetail.ProductId = item.ProductId;
                    //orderdetail.OrderId = lastorder.Id;
                    product.Quantity -= item.Quantity;
                    _context.Add(orderdetail);
                    await _context.SaveChangesAsync();
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                    _context.Carts.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}