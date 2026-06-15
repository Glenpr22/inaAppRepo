using inaApp.Common.interfaces;

using inaApp.Data;
using inaApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace inaApp.Repository
{
    //This class has the responsiblity to implement
    //the interface generic and connect with the database
    public class ClienteRepository : IGenericRepository<Cliente>
    {
        //we need to inject the context 
        public readonly ApplicationDbContext _context;


        //we can now access to the database and context in this repository
        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;

        }//end method constructor


        //AsNoTracking means that we just want to read the data in the database   
        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            try
            {
                //return await _context.Productos.AsNoTracking().Where(p => p.Estado == true).ToListAsync();
                return await _context.Clientes.AsNoTracking().Where(c => c.Estado == true).ToListAsync();

            }
            catch (Exception ex)
            {

                throw new Exception($"Error al consultar clientes en la base de datos: {ex.Message}", ex);
            }

        }//end method getAll


        //SingleOrDefaultAsync: search in the database for a specific result
        //just one result if exist or if not exist return null 
        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            try
            {

                return await _context.Clientes.AsNoTracking().Where(c => c.IdCliente == id && c.Estado == true).SingleOrDefaultAsync();

            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener el cliente por ID: ", ex);

            }
        }//end method getById


        //We use await because it's an asyc method and we need it.
        //to comunicate with the database and save information received as parameters 
        public async Task<Cliente> CrearAsync(Cliente entity)
        {
            try
            {
                await _context.Clientes.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {

                throw new Exception("Error al crear el cliente: ", ex);
            }
        }//end method create


        //recieve the entity as parameter
        //update the entity in the database  
        public async Task<Cliente> ActualizarAsync(Cliente entity)
        {
            try
            {

                _context.Clientes.Update(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente: ", ex);

            }

        }//end method update


        //first: search client by id 
        //second: delete logic by change the state 
        //third: update the client with the new state
        public async Task<bool> EliminarAsync(int id)
        {
            try
            {
                var cliente = await _context.Clientes
                    .FirstOrDefaultAsync(c => c.IdCliente == id && c.Estado == true);

                if (cliente == null)
                    return false;

                cliente.Estado = false;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente.", ex);
            }
        }//end method delete

        public Task<bool> ExisteNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }
    }//end class
}//end 
