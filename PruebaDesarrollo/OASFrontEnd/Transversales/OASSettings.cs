using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OASFrontEnd.Transversales
{
    public class OASSettings
    {
        public static string DataServiceUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["DataServiceUrl"] + "/api/";
            }
        }
    }
}