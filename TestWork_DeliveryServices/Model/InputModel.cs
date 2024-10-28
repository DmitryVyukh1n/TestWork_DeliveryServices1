using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWork_DeliveryServices.Model
{
    internal class InputModel
    {
        public string OrderNumber { get; set; }
        public int OrderWeight { get; set; }
        public string OrderRegion { get; set; }
        public DateTime OrderDeliveryTime { get; set; }
    }
}
