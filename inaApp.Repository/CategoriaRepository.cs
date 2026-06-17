using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inaApp.Common.interfaces;
using inaApp.Data;
using inaApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace inaApp.Repository
{
    public class CategoriaRepository : IGenericRepository<Categoria>
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Categorias
                .AnyAsync(x => x.Nombre == nombre);
        }

        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            try
            {
                return await _context.Categorias
                    .Where(x => x.Id == id)
                    .SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Categoria>> ObtenerTodosAsync()
        {
            try
            {
                return await _context.Categorias
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Categoria> CrearAsync(Categoria entity)
        {
            try
            {
                _context.Categorias.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Categoria> ActualizarAsync(Categoria entity)
        {
            try
            {
                _context.Categorias.Update(entity);
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
                var categoria = await ObtenerPorIdAsync(id);

                if (categoria == null)
                    return false;

                _context.Categorias.Remove(categoria);
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
