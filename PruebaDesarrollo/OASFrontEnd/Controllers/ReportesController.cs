using Entidades;
using OASFrontEnd.Transversales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OASFrontEnd.Controllers
{
    public class ReportesController : Controller
    {
        ApiRequest oasApi = new ApiRequest();
        private readonly string serviceName = "Reportes/";
        private readonly string serviceNameEstado = "Estados/";
        private readonly string serviceNameResponsable = "Responsables/";

        // GET: Reportes
        public async Task<ActionResult> ActividadesPorResponsable()
        {
            await PopulateDropDownList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ActividadesPorResponsable(int idResponsable)
        {
            var resultado = await oasApi.GetData<List<Actividad>>(serviceName, idResponsable, "idResponsable");
            if (resultado == null)
            {
                return HttpNotFound();
            }
            await PopulateDropDownList();
            return View(resultado);
        }


        public async Task<ActionResult> ActividadesPorEstado()
        {
            await PopulateDropDownList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ActividadesPorEstado(int idEstado)
        {
            var resultado = await oasApi.GetData<List<Actividad>>(serviceName, idEstado, "idEstado");
            if (resultado == null)
            {
                return HttpNotFound();
            }
            await PopulateDropDownList();
            return View(resultado);
        }


        private async Task PopulateDropDownList()
        {
            var dataEstado = await oasApi.GetData<List<Estado>>(serviceNameEstado, null);
            var dataResponsable = await oasApi.GetData<List<Responsable>>(serviceNameResponsable, null);

            ViewBag.IdEstado = new SelectList(dataEstado, "IdEstado", "NombreEstado");
            ViewBag.IdResponsable = new SelectList(dataResponsable, "IdResponsable", "Nombres");
        }
    }
}