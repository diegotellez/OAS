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
    public class ResponsablesController : ApiController
    {
        private OASContext db = new OASContext();

        // GET: api/Responsables
        public IQueryable<Responsable> GetResponsables()
        {
            return db.Responsables;
        }

        // GET: api/Responsables/5
        [ResponseType(typeof(Responsable))]
        public IHttpActionResult GetResponsable(int id)
        {
            Responsable responsable = db.Responsables.Find(id);
            if (responsable == null)
            {
                return NotFound();
            }

            return Ok(responsable);
        }

        // PUT: api/Responsables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResponsable(Responsable responsable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(responsable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(true);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponsableExists(responsable.IdResponsable))
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

        // POST: api/Responsables
        [ResponseType(typeof(Responsable))]
        public IHttpActionResult PostResponsable(Responsable responsable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Responsables.Add(responsable);
            db.SaveChanges();

            return Ok(true);
        }

        // DELETE: api/Responsables/5
        [ResponseType(typeof(Responsable))]
        public IHttpActionResult DeleteResponsable(int id)
        {
            Responsable responsable = db.Responsables.Find(id);
            if (responsable == null)
            {
                return NotFound();
            }

            db.Responsables.Remove(responsable);
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

        private bool ResponsableExists(int id)
        {
            return db.Responsables.Count(e => e.IdResponsable == id) > 0;
        }
    }
}