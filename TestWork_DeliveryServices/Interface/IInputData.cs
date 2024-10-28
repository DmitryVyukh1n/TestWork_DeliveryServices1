using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork_DeliveryServices.Model;

namespace TestWork_DeliveryServices.Interface
{
    internal interface IInputData
    {
        public Task<List<InputModel>> GetInputData(StartParametrs parametr);
    }
}
