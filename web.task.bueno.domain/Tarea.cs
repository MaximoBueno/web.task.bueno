namespace web.task.bueno.domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tarea")]
    public partial class Tarea
    {
        public int Id { get; set; }

        public int? IdUsuario { get; set; }

        [StringLength(150)]
        public string Titulo { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuario Usuario { get; set; }
    }
}
