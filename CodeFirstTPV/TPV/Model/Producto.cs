using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV.Model
{
    public class Producto
    {
        public Producto()
        {
            LineasVenta = new HashSet<LineaVenta>();
        }
        public int ProductoID { get; set; }
        public string nombre { get; set; }
        public int descuento { get; set; }
        public double precio { get; set; }
        public string imagen { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<LineaVenta> LineasVenta { get; set; }
    }
}
