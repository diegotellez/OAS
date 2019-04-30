using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OASFrontEnd.Transversales
{
    public class ERespuestaApi
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
        public string Fuente { get; set; }
        public dynamic Llave { get; set; }
    }
}