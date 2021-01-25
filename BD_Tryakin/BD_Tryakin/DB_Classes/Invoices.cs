using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_Tryakin.DB_Classes
{
    [Table("Invoices")]
    public class Invoices
    {
        public Invoices() { Products = new HashSet<Products>(); }

        [Key]
        public int ID_Invoice { get; set; }

        public string Name_Invoice { get; set; }
        public DateTime Date_Invoice { get; set; }
        public int ProductCount_Invoice { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
