using System.Collections.Generic;
using System.Linq;
using Project_SEM2_HNDShop.Data;
using Project_SEM2_HNDShop.DTO;
using Project_SEM2_HNDShop.Models;

namespace Project_SEM2_HNDShop.Services
{
    public class Repository
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public Order GetLastOrderByUserId(int userId)
        {
            var query = _context.Orders.Where(o => o.UserId == userId).OrderByDescending(o => o.Id).Take(1);
            Order order = new Order();
            order = query.FirstOrDefault();
            return order;
        }

        public List<CartViewDto> GetListCartByUserId(int userId)
        {
            var query = from cart in _context.Carts.ToList().Where(c => c.UserId == userId)
                        from product in _context.Products.ToList()
                        where cart.ProductId == product.Id
                        from promo in _context.Promotions.ToList()
                        where product.PromoId == promo.Id
                        select new CartViewDto()
                        {
                            Id = cart.Id,
                            product = product,
                            DiscountPercent = promo.DiscountPercent,
                            Quantity = cart.Quantity
                        };
            return query.ToList();
        }

        public List<ProductDto> GetListProduct()
        {
            var query = from cate in _context.Categories.ToList()
                        from product in _context.Products.ToList()
                        where cate.Id == product.CateId
                        from subrand in _context.SubBrands.ToList()
                        where product.SubBrandId == subrand.Id
                        from brand in _context.Brands.ToList()
                        where subrand.BrandId == brand.Id
                        from promo in _context.Promotions.ToList()
                        where product.PromoId == promo.Id
                        select new ProductDto()
                        {
                            product = product,
                            category = cate,
                            subBrand = subrand,
                            brand = brand,
                            promotion = promo
                        };
            return query.ToList();
        }
        public ProductDto GetDetailProduct(int id)
        {
            var query = from cate in _context.Categories.ToList()
                        from product in _context.Products.ToList().Where(p => p.Id == id)
                        where cate.Id == product.CateId
                        from subrand in _context.SubBrands.ToList()
                        where product.SubBrandId == subrand.Id
                        from brand in _context.Brands.ToList()
                        where subrand.BrandId == brand.Id
                        from promo in _context.Promotions.ToList()
                        where product.PromoId == promo.Id
                        select new ProductDto()
                        {
                            product = product,
                            category = cate,
                            subBrand = subrand,
                            brand = brand,
                            promotion = promo
                        };
            return query.FirstOrDefault();
        }
    }
}