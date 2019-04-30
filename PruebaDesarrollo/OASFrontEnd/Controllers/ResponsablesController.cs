using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Entidades;
using OASFrontEnd.Transversales;

namespace OASFrontEnd.Controllers
{
    public class ResponsablesController : Controller
    {
        private readonly string serviceName = "Responsables/";
        ApiRequest oasApi = new ApiRequest();
        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var data = await oasApi.GetData<List<Responsable>>(serviceName, null);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdResponsable,Nombres,Apellidos,Email,Telefono")] Responsable responsable)
        {
            if (ModelState.IsValid)
            {
                var result = await oasApi.SendData(serviceName, responsable, WebApi.RequestType.Post);
                return RedirectToAction("Index");
            }
            else
            {
                return View(responsable);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsable responsable = await oasApi.GetData<Responsable>(serviceName, id);
            if (responsable == null)
            {
                return HttpNotFound();
            }
            return View(responsable);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Responsable responsable)
        {
            if (ModelState.IsValid)
            {
                var result = await oasApi.SendData(serviceName, responsable, WebApi.RequestType.Put);
                return RedirectToAction("Index");
            }
            else
            {
                return View(responsable);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Responsable responsable = await oasApi.GetData<Responsable>(serviceName, id);
            if (responsable == null)
            {
                return HttpNotFound();
            }
            return View(responsable);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await oasApi.SendData(serviceName, id, WebApi.RequestType.Delete);
            return RedirectToAction("Index");
        }
    }
}
