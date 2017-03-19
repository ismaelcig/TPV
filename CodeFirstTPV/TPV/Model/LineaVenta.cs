using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV.Model
{
    public class LineaVenta
    {
        [Key, Column(Order = 0)]
        public int ProductoID { get; set; }
        [Key, Column(Order = 1)]
        public int VentaID { get; set; }
        public int cantidad { get; set; }
        public string precioTotal { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Venta Venta { get; set; }
    }
}
