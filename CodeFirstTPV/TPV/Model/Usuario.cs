using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV.Model
{
    public class Usuario
    {
        public Usuario()
        {
            Ventas = new HashSet<Venta>();
        }
        public int UsuarioID { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public bool admin { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
