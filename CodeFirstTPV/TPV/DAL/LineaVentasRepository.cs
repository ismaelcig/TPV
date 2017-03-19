using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Model;

namespace TPV.DAL
{
    public class LineaVentasRepository: GenericRepository<LineaVenta>
    {
        public LineaVentasRepository(TiendaContext context)
            : base(context)
        {
        }
        public IEnumerable<LineaVenta> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (lV => lV.ProductoID.ToString().Contains(buscado.ToUpper())
                                         || lV.VentaID.ToString().Contains(buscado.ToUpper())
                                         )
                                         );
            }
            else return Get();
        }
    }
}
