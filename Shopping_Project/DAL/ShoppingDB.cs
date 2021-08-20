using BE;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ShoppingDB : DbContext
    {
        public ShoppingDB() : base("ShoppingDataBase") {}
        public DbSet<QrCode> QrCodes { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Url> DownloadUrls { get; set; }


    }
}
