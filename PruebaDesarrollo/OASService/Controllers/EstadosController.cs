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
    public class EstadosController : ApiController
    {
        private OASContext db = new OASContext();

        // GET: api/Estados
        public IQueryable<Estado> GetEstados()
        {
            return db.Estados;
        }

        // GET: api/Estados/5
        [ResponseType(typeof(Estado))]
        public IHttpActionResult GetEstado(int id)
        {
            Estado estado = db.Estados.Find(id);
            if (estado == null)
            {
                return NotFound();
            }

            return Ok(estado);
        }

        // PUT: api/Estados/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstado(Estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(estado).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(estado.IdEstado))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Estados
        [ResponseType(typeof(Estado))]
        public IHttpActionResult PostEstado(Estado estado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Estados.Add(estado);
            db.SaveChanges();

            return Ok(true);
        }

        // DELETE: api/Estados/5
        [ResponseType(typeof(Estado))]
        public IHttpActionResult DeleteEstado(int id)
        {
            Estado estado = db.Estados.Find(id);
            if (estado == null)
            {
                return NotFound();
            }

            db.Estados.Remove(estado);
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

        private bool EstadoExists(int id)
        {
            return db.Estados.Count(e => e.IdEstado == id) > 0;
        }
    }
}