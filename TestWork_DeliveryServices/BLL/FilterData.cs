using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork_DeliveryServices.Interface;
using TestWork_DeliveryServices.Model;

namespace TestWork_DeliveryServices.BLL
{
    internal class FilterData : IFilterData
    {
        private DateTime? _lastDeliveryDateTime;
        private readonly Loggers _logger;
        public FilterData(Loggers logger)
        {
            _logger = logger;
        }

        public async Task<List<InputModel>> GetFilterData(StartParametrs parametrs, List<InputModel> inputModels)
        {
            _lastDeliveryDateTime = parametrs.FirstDeliveryDateTime.Value.AddMinutes(30);
            _logger.Info("Запускаем фильрацию по списку");
            return await GetFilter(parametrs,inputModels);
        }

        private async Task<List<InputModel>> GetFilter(StartParametrs parametrs,List<InputModel> inputModels)
        {
          
            var result = inputModels.Where(x=>x.OrderRegion== parametrs.CityDistinct &&
                x.OrderDeliveryTime>= parametrs.FirstDeliveryDateTime && x.OrderDeliveryTime<= _lastDeliveryDateTime).OrderBy(x => x.OrderDeliveryTime).ToList();
           
            return result;
        }
    }
}
