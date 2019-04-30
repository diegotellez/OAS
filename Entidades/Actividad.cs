using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Actividad
    {
        [Key]
        public int IdActividad { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaCreacion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaLimite { get; set; }

        [ForeignKey("IdResponsable")]
        public Responsable ResponsableActividad { get; set; }

        [Required]
        public int IdResponsable { get; set; }

        public string Descripcion { get; set; }

        [ForeignKey("IdEstado")]
        public Estado EstadoActividad { get; set; }

        [Required]
        public int IdEstado { get; set; }
    }
}
