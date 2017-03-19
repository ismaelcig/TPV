using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Model;

namespace TPV.DAL
{
    public class VentasRepository : GenericRepository<Venta>
    {
        public VentasRepository(TiendaContext context)
            : base(context)
        {
        }
        public IEnumerable<Venta> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (sell => sell.VentaID.ToString().Contains(buscado.ToUpper())
                                         || sell.Usuario.ToString().Contains(buscado.ToUpper())
                                         )
                                         );
            }
            else return Get();
        }
    }
}
