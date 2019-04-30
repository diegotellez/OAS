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
    public class ActividadesController : ApiController
    {
        private OASContext db = new OASContext();

        // GET: api/Actividades
        public IQueryable<Actividad> GetActividades()
        {
            return db.Actividades.Include(a=>a.ResponsableActividad).Include(a=>a.EstadoActividad);
        }

        // GET: api/Actividades/5
        [ResponseType(typeof(Actividad))]
        public IHttpActionResult GetActividad(int id)
        {
            Actividad actividad = db.Actividades.Include(a => a.ResponsableActividad).Include(a => a.EstadoActividad)
                .Where(a=>a.IdActividad == id).FirstOrDefault();
            if (actividad == null)
            {
                return NotFound();
            }

            return Ok(actividad);
        }

        // PUT: api/Actividades/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutActividad(Actividad actividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(actividad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadExists(actividad.IdActividad))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Actividades
        [ResponseType(typeof(Actividad))]
        public IHttpActionResult PostActividad(Actividad actividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Actividades.Add(actividad);
            db.SaveChanges();

            return Ok(true);
        }

        // DELETE: api/Actividades/5
        [ResponseType(typeof(Actividad))]
        public IHttpActionResult DeleteActividad(int id)
        {
            Actividad actividad = db.Actividades.Find(id);
            if (actividad == null)
            {
                return NotFound();
            }

            db.Actividades.Remove(actividad);
            db.SaveChanges();

            return Ok(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActividadExists(int id)
        {
            return db.Actividades.Count(e => e.IdActividad == id) > 0;
        }
    }
}