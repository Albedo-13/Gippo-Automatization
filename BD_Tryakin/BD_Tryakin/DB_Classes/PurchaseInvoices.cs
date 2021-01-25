using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_Tryakin.DB_Classes
{
    [Table("PurchaseInvoices")]
    public class PurchaseInvoices
    {
        public PurchaseInvoices() { Products = new HashSet<Products>(); }

        [Key]
        public int ID_PurchaseInvoice { get; set; }

        public string Name_PurchaseInvoice { get; set; }
        public DateTime Date_PurchaseInvoice { get; set; }
        public int ProductCount_PurchaseInvoice { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
