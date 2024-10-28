using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork_DeliveryServices.Interface;
using TestWork_DeliveryServices.Model;

namespace TestWork_DeliveryServices.BLL
{
    internal class TestWorks
    {
        private readonly IParametrs _parametrs;
        private readonly IInputData _inputData;
        private readonly IFilterData _filterData;
        private readonly ISave _save;
        private readonly Loggers _logger;

        public TestWorks(IParametrs parametrs,IInputData inputData, IFilterData filterData, ISave save, Loggers logger)
        {
            _parametrs = parametrs;
            _inputData = inputData;
            _filterData = filterData;
            _save = save;
            _logger = logger;
        }

        public async Task TestWorkProcess()
        {
            var parametr = await _parametrs.GetStartParametrs();
            var file = await _inputData.GetInputData(parametr);
            if (file == null)
            {
                Console.WriteLine("Отсутсвует файл по указанному пути!");
                return;
            }
            var filter = await _filterData.GetFilterData(parametr,file);
            await _save.Saves(parametr,filter);
        }
    }
}
