using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Model;

namespace TPV.DAL
{
    public class UsuariosRepository: GenericRepository<Usuario>
    {
        public UsuariosRepository(TiendaContext context)
            : base(context)
        {
        }
        public IEnumerable<Usuario> GetFiltrado(String buscado)
        {
            if (!string.IsNullOrWhiteSpace(buscado))
            {
                return Get(filter: (user => user.UsuarioID.ToString().Contains(buscado.ToUpper())
                                         || user.nombre.ToUpper().Contains(buscado.ToUpper())
                                         || user.password.ToUpper().Contains(buscado.ToUpper())
                                         || user.admin.ToString().Contains(buscado.ToUpper())
                                         )
                                         );
            }
            else return Get();
        }
    }
}
