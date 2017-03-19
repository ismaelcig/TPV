using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV.Model
{
    public class Venta
    {
        public Venta()
        {
            LineasVenta = new HashSet<LineaVenta>();
        }
        public int VentaID { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<LineaVenta> LineasVenta { get; set; }
    }
}
