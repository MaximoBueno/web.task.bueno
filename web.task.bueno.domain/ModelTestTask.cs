using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace web.task.bueno.domain
{
    public partial class ModelTestTask : DbContext
    {
        public ModelTestTask()
            : base("name=ModelTestTask")
        {
        }

        public ModelTestTask(string ConnectionString)
           : base(ConnectionString)
        {
        }

        public virtual DbSet<Tarea> Tarea { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Tarea>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.NombreCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Correo)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Clave)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Tarea)
                .WithOptional(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario);
        }
    }
}
