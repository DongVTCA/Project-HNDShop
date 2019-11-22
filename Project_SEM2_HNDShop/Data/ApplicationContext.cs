using Microsoft.EntityFrameworkCore;
using Project_SEM2_HNDShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_SEM2_HNDShop.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<SubBrand> SubBrands { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Orderdetail> Orderdetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Configuation> Configuations { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
