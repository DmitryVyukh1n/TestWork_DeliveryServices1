using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWork_DeliveryServices.Model
{
    internal class StartParametrs
    {
        public string CityDistinct {  get; set; }
        public DateTime? FirstDeliveryDateTime { get; set; }
       // public string DeliveryLog { get; set; }
        public string DeliveryOrder {  get; set; }
        public string PathFile { get; set; }
    }
}
