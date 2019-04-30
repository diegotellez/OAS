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
    public class EstadosController : Controller
    {
        ApiRequest oasApi = new ApiRequest();
        private readonly string serviceName = "Estados/";

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var data = await oasApi.GetData<List<Estado>>(serviceName, null);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdEstado,NombreEstado")] Estado estado)
        {
            if (ModelState.IsValid)
            {
                var result = await oasApi.SendData(serviceName, estado, WebApi.RequestType.Post);
                return RedirectToAction("Index");
            }
            else
            {
                return View(estado);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await oasApi.GetData<Estado>(serviceName, id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Estado estado)
        {
            if (ModelState.IsValid)
            {
                var result = await oasApi.SendData(serviceName, estado, WebApi.RequestType.Put);
                return RedirectToAction("Index");
            }
            else
            {
                return View(estado);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estado estado = await oasApi.GetData<Estado>(serviceName, id);
            if (estado == null)
            {
                return HttpNotFound();
            }
            return View(estado);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await oasApi.SendData(serviceName, id, WebApi.RequestType.Delete);
            return RedirectToAction("Index");
        }
    }
}
