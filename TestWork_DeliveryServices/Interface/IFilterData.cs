using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork_DeliveryServices.Model;

namespace TestWork_DeliveryServices.Interface
{
    internal interface IFilterData
    {
        public Task<List<InputModel>> GetFilterData(StartParametrs parametr, List<InputModel> inputModels);
    }
}
