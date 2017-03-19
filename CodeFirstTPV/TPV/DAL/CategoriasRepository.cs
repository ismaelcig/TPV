using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Model;

namespace TPV.DAL
{
    public class CategoriasRepository : GenericRepository<Categoria>
    {
        public CategoriasRepository(TiendaContext context)
            : base(context)
        {
        }
        public IEnumerable<Categoria> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (cat => cat.CategoriaID.ToString().Contains(buscado.ToUpper())
                                         || cat.nombre.ToUpper().Contains(buscado.ToUpper())
                                         )
                                         );
            }
            else return Get();
        }
    }
}
