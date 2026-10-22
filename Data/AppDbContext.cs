using GestorTareas.Models;
using Microsoft.EntityFrameworkCore;

namespace GestorTareas.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Relación explícita con borrado en cascada
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tareas)
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data: Usuarios de prueba
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1,Nombre = "Administrador Sistema",Email = "admin@workspace.com"  },
                new Usuario { Id = 2,Nombre = "Ana Gómez (Dev)",Email = "ana.dev@workspace.com"  } 
            );

            // Seed Data: Tareas de prueba iniciales
            modelBuilder.Entity<Tarea>().HasData(
                new Tarea { Id = 1, Titulo = "Diseñar Arquitectura base",Descripcion = "Crear el AppDbContext y capa servicios.", FechaVencimiento = new DateTime(2026,11,1),Estado = "Completada",UsuarioId = 1},
                new Tarea { Id = 2, Titulo = "Desarrollar Frontend Tablero",Descripcion = "Implementar CSS Grid para las 3 columnas.", FechaVencimiento = new DateTime(2026, 11, 15), Estado = "En Progreso", UsuarioId = 2 },
                new Tarea { Id = 3, Titulo = "Integrar Fetch API", Descripcion = "Cambiar los etados de forma asíncrona.", FechaVencimiento = new DateTime(2026, 11, 30), Estado = "Pendiente", UsuarioId = 2 }
            );

        }        
        
    }
}
