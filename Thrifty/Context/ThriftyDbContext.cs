using Microsoft.EntityFrameworkCore;
using Thrifty.Models;

namespace Thrifty.Context
{
    public class ThriftyDbContext : DbContext
    {
        private readonly List<Role> objRole = new List<Role>()
        {
            new Role
            {
                id= 1,
                name = "Seller",
            },
            new Role
            {
                id= 2,
                name = "Admin",
            },
            new Role
            {
                id= 3,
                name = "Customer",
            }
        };
        private readonly List<ItemCategory> objItemType = new List<ItemCategory>()
        {
            new ItemCategory
            {
                id= 1,
                name = "T-Shirt",
            },
            new ItemCategory
            {
                id= 2,
                name = "Pants",
            },
             new ItemCategory
            {
                id= 3,
                name = "Shorts",
            },
              new ItemCategory
            {
                id= 4,
                name = "Dresses",
            },
              new ItemCategory
            {
                id= 5,
                name = "Shoes",
            }
        };
        private readonly List<City> objCity = new List<City>()
        {
            new City
            {
                id = 1,
                name = "Amman"
            },
            new City
            {
                id = 2,
                name = "Aqaba"
            },
            new City
            {
                id = 3,
                name = "Madaba"
            },
            new City
            {
                id = 4,
                name = "Irbid"
            },
            new City
            {
                id = 5,
                name = "Salt"
            },
            new City
            {
                id = 6,
                name = "Zarqa"
            },
            new City
            {
                id = 7,
                name = "Jerash"
            },
            new City
            {
                id = 8,
                name = "Ma'an"
            },
            new City
            {
                id = 9,
                name = "Al-Mafraq"
            }
        };
        private readonly List<User> objUser = new List<User>()
        {
            new User
            {
                id = 1,
                email = "Admin@Thrifty.com",
                password = "123",
                fullName = "Admin",
                mobileNumber = 10000000000,
                roleId = 2,
                cityId = 1,
            }

        };
        private readonly List<OrderStatus> objOrderStatus = new List<OrderStatus>()
        {
            new OrderStatus() {
                id = 1,
                Name = "Pending"
            },
            new OrderStatus() {
                id = 2,
                Name = "Accepted"
            },
            new OrderStatus() {
                id = 3,
                Name = "On The Way"
            },
            new OrderStatus() {
                id = 4,
                Name = "Delivered"
            },
            new OrderStatus() {
                id = 5,
                Name = "Canceled"
            },
            new OrderStatus() {
                id = 6,
                Name = "Rejected"
            }
        };

        
        public ThriftyDbContext() : base() { }
        public ThriftyDbContext(DbContextOptions<ThriftyDbContext> options) : base(options)
        {


        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(objRole);
            modelBuilder.Entity<ItemCategory>().HasData(objItemType);
            modelBuilder.Entity<City>().HasData(objCity);
            modelBuilder.Entity<User>().HasData(objUser);
            modelBuilder.Entity<OrderStatus>().HasData(objOrderStatus);

            base.OnModelCreating(modelBuilder);
        }




        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImages> ItemImages { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<ShopingCart> ShopingCart { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
