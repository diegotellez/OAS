using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Estado
    {
        [Key]
        public int IdEstado { get; set; }

        [Required]
        public string NombreEstado { get; set; }
    }
}
