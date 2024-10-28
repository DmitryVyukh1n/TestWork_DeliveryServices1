using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TestWork_DeliveryServices.BLL
{
    internal class Helper
    {
        public static string GetConst(string Key)
        {
            return ConfigurationManager.AppSettings[Key];
        }
    }
}
