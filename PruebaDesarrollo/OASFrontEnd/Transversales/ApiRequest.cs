using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OASFrontEnd.Transversales
{
    public class ApiRequest
    {
        public async Task<T> GetData<T>(string serviceName, int? id)
        {
            var respuestaApiConsulta = await WebApi.ConsumirWebApiGetAsync<T>(
                        OASSettings.DataServiceUrl + serviceName + "/" + id
                );

            return respuestaApiConsulta;
        }

        public async Task<bool> SendData(string serviceName, object entityData, WebApi.RequestType requestType)
        {
            var respuestaApiConsulta = await WebApi.ConsumirWebApiPostAsync<bool>(
                        OASSettings.DataServiceUrl + serviceName, entityData, requestType
                );

            return respuestaApiConsulta;
        }
    }
}