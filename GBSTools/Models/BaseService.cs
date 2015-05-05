using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSTools.Models
{
    public class BaseService
    {
        string WebDirectorConnectString = "";
        string FcAppName = "";
        public BaseService()
        {
            readSettings();
        }
        private void readSettings()
        {
            string W3Exec = System.Configuration.ConfigurationManager.AppSettings["w3exec"];
            string W3ServerPool = System.Configuration.ConfigurationManager.AppSettings["w3ServerPool"];
            if (W3ServerPool == null) W3ServerPool = "default";
            string Account = System.Configuration.ConfigurationManager.AppSettings["account"];

            WebDirectorConnectString = System.Configuration.ConfigurationManager.AppSettings["webDirectorConnectString"];
            FcAppName = W3Exec + "&w3ServerPool=" + W3ServerPool;
        }
    }
}