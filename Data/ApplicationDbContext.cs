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
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        //fluid API para configurar el modelo de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relacion producto - categoria
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)              // 1 product, 1 category
                .WithMany(c => c.Productos)            // 1 category have muchos productos
                .HasForeignKey(p => p.CategoriaId);    // La FK en Producto

            //relacion cliente con tipo cliente

            //termina de definir aquello que no se puede definir con
            //dataAnotations con relaciones entre tablas, indices, etc
            base.OnModelCreating(modelBuilder);
        }



    }//end ApplicationDbContext
}
