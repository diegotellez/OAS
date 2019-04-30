using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entidades;

namespace DAL.Entities
{
    public class OASContext : DbContext
    {
        public OASContext() : base("oasSqlServerConnectionString")
        {
        }

        public DbSet<Estado> Estados { get; set; }

        public DbSet<Responsable> Responsables { get; set; }

        public DbSet<Actividad> Actividades { get; set; }
    }
}
