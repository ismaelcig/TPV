using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPV.DAL
{
    public class UnitOfWork
    {
        private TiendaContext context = new TiendaContext();
        private bool disposed = false;

        private CategoriasRepository categoriasRepository;
        private ProductosRepository productosRepository;
        private UsuariosRepository usuariosRepository;
        private VentasRepository ventasRepository;
        private LineaVentasRepository lineaVentasRepository;

        public CategoriasRepository CategoriasRepository
        {
            get
            {
                if (this.categoriasRepository == null)
                {
                    this.categoriasRepository = new CategoriasRepository(context);
                }
                return categoriasRepository;
            }
        }
        public ProductosRepository ProductosRepository
        {
            get
            {
                if (this.productosRepository == null)
                {
                    this.productosRepository = new ProductosRepository(context);
                }
                return productosRepository;
            }
        }
        public UsuariosRepository UsuariosRepository
        {
            get
            {
                if (this.usuariosRepository == null)
                {
                    this.usuariosRepository = new UsuariosRepository(context);
                }
                return usuariosRepository;
            }
        }
        public VentasRepository VentasRepository
        {
            get
            {
                if (this.ventasRepository == null)
                {
                    this.ventasRepository = new VentasRepository(context);
                }
                return ventasRepository;
            }
        }
        public LineaVentasRepository LineaVentasRepository
        {
            get
            {
                if (this.lineaVentasRepository == null)
                {
                    this.lineaVentasRepository = new LineaVentasRepository(context);
                }
                return lineaVentasRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
