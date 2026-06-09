using inaApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;   


namespace inaApp.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }//end constructor



        //definir las tablas de la base de datos
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

    }//end db
}
