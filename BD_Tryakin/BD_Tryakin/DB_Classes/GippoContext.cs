using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BD_Tryakin.DB_Classes
{
    public class GippoContext : DbContext
    {
        public DbSet<ProductTypes> ProductTypes { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<PurchaseInvoices> PurchaseInvoices { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
