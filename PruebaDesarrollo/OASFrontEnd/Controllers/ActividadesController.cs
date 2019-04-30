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
    public class ActividadesController : Controller
    {
        ApiRequest oasApi = new ApiRequest();
        private readonly string serviceName = "Actividades/";
        private readonly string serviceNameEstado = "Estados/";
        private readonly string serviceNameResponsable = "Responsables/";

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var data = await oasApi.GetData<List<Actividad>>(serviceName, null);
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await PopulateDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdActividad,FechaCreacion,FechaLimite,IdResponsable,Descripcion,IdEstado")] Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                var result = await oasApi.SendData(serviceName, actividad, WebApi.RequestType.Post);
                return RedirectToAction("Index");
            }
            else
            {
                await PopulateDropDownList();
                return View(actividad);
            }
        }

        private async Task PopulateDropDownList()
        {
            var dataEstado = await oasApi.GetData< List<Estado>>(serviceNameEstado, null);
            var dataResponsable = await oasApi.GetData< List<Responsable>>(serviceNameResponsable, null);

            ViewBag.IdEstado = new SelectList(dataEstado, "IdEstado", "NombreEstado");
            ViewBag.IdResponsable = new SelectList(dataResponsable, "IdResponsable", "Nombres");
        }


        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = await oasApi.GetData<Actividad>(serviceName, id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            await PopulateDropDownList();
            return View(actividad);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                var result = await oasApi.SendData(serviceName, actividad, WebApi.RequestType.Put);
                return RedirectToAction("Index");
            }
            else
            {
                await PopulateDropDownList();
                return View(actividad);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actividad actividad = await oasApi.GetData<Actividad>(serviceName, id);
            if (actividad == null)
            {
                return HttpNotFound();
            }
            return View(actividad);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await oasApi.SendData(serviceName, id, WebApi.RequestType.Delete);
            return RedirectToAction("Index");
        }
    }
}
