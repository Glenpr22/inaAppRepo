using inaApp.Common.interfaces;
using inaApp.Data;
using inaApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inaApp.Repository
{
    public class ProductoRepository : IGenericRepository<Producto>
    {

        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Productos
                .AnyAsync(x => x.Nombre == nombre && x.Estado);
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            try
            {
                return await _context.Productos.Where(x => x.Id == id && x.Estado == true)
                    .SingleOrDefaultAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<List<Producto>> ObtenerTodosAsync()
        {
            try
            {
                //  return await _context.Producto.Where(x => x.Estado==true).ToListAsync();
                return await _context.Productos.Where(x => x.Estado).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//end

        public async Task<Producto> CrearAsync(Producto entity)
        {
            try
            {
                _context.Productos.Add(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Producto> ActualizarAsync(Producto entity)
        {
            try
            {
                _context.Productos.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var producto = await ObtenerPorIdAsync(id);
                if (producto == null)
                {
                    return false;

                }
                //borrado logic
                producto.Estado = false;
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
