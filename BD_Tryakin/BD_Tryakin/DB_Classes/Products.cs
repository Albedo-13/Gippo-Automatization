using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_Tryakin.DB_Classes
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int Id_Product { get; set; }

        public string Name_Product { get; set; }
        public int ProductCount_Product { get; set; }
        public string Description_Product { get; set; }
        public decimal Price_Product { get; set; }

        [ForeignKey("ProductTypes")]
        public int Id_ProductType { get; set; }
        public virtual ProductTypes ProductTypes { get; set; }

        [ForeignKey("Invoices")]
        public int? ID_Invoice { get; set; }
        public virtual Invoices Invoices { get; set; }

        [ForeignKey("PurchaseInvoices")]
        public int? ID_PurchaseInvoice { get; set; }
        public virtual PurchaseInvoices PurchaseInvoices { get; set; }

        [ForeignKey("Users")]
        public string Login_User { get; set; }
        public virtual Users Users { get; set; }
    }
}
