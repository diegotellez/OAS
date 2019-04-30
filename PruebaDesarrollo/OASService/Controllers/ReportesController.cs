using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL.Entities;
using Entidades;

namespace OASService.Controllers
{
    public class ReportesController : ApiController
    {
        private OASContext db = new OASContext();

        [HttpGet]
        public IQueryable<Actividad> ActividadesPorEstado(int idEstado)
        {
            return db.Actividades.Include(a => a.EstadoActividad).Include(a => a.ResponsableActividad)
                .Where(a => a.IdEstado== idEstado);
        }

        [HttpGet]
        public IQueryable<Actividad> ActividadesPorResponsable(int idResponsable)
        {
            return db.Actividades.Include(a => a.EstadoActividad).Include(a => a.ResponsableActividad)
                .Where(a => a.IdResponsable == idResponsable);
        }
    }
}
