using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BD_Tryakin.DB_Classes
{
    [Table("ProductTypes")]
    public class ProductTypes
    {
        public ProductTypes() { Products = new HashSet<Products>(); }

        [Key]
        public int Id_ProductType { get; set; }

        public string Name_ProductType { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
