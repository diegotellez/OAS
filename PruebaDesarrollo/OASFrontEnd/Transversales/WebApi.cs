using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OASFrontEnd.Transversales
{
    public class WebApi
    {
        public static async Task<dynamic> ConsumirWebApiGetAsync<T>(string url)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (((int)response.StatusCode).Equals(200) || ((int)response.StatusCode).Equals(201))
                    {
                        if (typeof(T) == typeof(string))
                            return await response.Content.ReadAsStringAsync();
                        else
                            return await response.Content.ReadAsAsync<T>();
                    }
                    else if (((int)response.StatusCode) != 513)
                    {
                        return new ERespuestaApi()
                        {
                            Codigo = ((int)response.StatusCode).ToString(),
                            Mensaje = await response.Content.ReadAsStringAsync(),
                            Fuente = "WM",
                        };
                    }
                    else
                    {
                        return await response.Content.ReadAsAsync<ERespuestaApi>();
                    }
                }
                catch (Exception e)
                {
                    return new ERespuestaApi()
                    {
                        Codigo = "500",
                        Mensaje = "Error en el consumo del servicio",
                        Fuente = "WM",
                    };
                }
            }
        }

        public static async Task<dynamic> ConsumirWebApiPostAsync<T>(string url, object data, RequestType requestType)
        {

            using (var client = new HttpClient())
            {
                var bodyData = JsonConvert.SerializeObject(data).ToString();
                var content = new StringContent(bodyData, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = null;

                    switch (requestType)
                    {
                        case RequestType.Post:
                            response = await client.PostAsync(url, content);
                            break;
                        case RequestType.Put:
                            response = await client.PutAsync(url, content);
                            break;
                        case RequestType.Delete:
                            response = await client.DeleteAsync(url+ data);
                            break;
                        default:
                            break;
                    }

                    if (((int)response.StatusCode).Equals(200) || ((int)response.StatusCode).Equals(201))
                    {
                        if (typeof(T) == typeof(string))
                            return await response.Content.ReadAsStringAsync();
                        else
                            return await response.Content.ReadAsAsync<T>();
                    }
                    else if (((int)response.StatusCode).Equals(500))
                    {
                        return new ERespuestaApi()
                        {
                            Codigo = "500",
                            Mensaje = "Error en el servicio",
                            Fuente = "WM",
                        };
                    }
                    else
                    {
                        if (data is IList)
                        {
                            return await response.Content.ReadAsAsync<List<ERespuestaApi>>();
                        }
                        else
                        {
                            return await response.Content.ReadAsAsync<ERespuestaApi>();
                        }
                    }
                }
                catch (Exception)
                {
                    return new ERespuestaApi()
                    {
                        Codigo = "500",
                        Mensaje = "Error en el consumo del servicio",
                        Fuente = "WM",
                    };
                }
            }
        }

        public enum RequestType
        {
            Post,
            Put,
            Delete
        }

        private static object ConvertToByteArray(object p)
        {
            throw new NotImplementedException();
        }

    }
}