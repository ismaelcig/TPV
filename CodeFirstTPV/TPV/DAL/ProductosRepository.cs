using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Model;

namespace TPV.DAL
{
    public class ProductosRepository : GenericRepository<Producto>
    {
        public ProductosRepository(TiendaContext context)
            : base(context)
        {
        }
        public IEnumerable<Producto> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (prod => prod.ProductoID.ToString().Contains(buscado.ToUpper())
                                         || prod.nombre.ToUpper().Contains(buscado.ToUpper())
                                         || prod.precio.ToString().Contains(buscado.ToUpper())
                                         || prod.descuento.ToString().Contains(buscado.ToUpper())
                                         )
                                         );
            }
            else return Get();
        }
    }
}
