using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV.Model
{
    public class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }
        public int CategoriaID { get; set; }
        public string nombre { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
