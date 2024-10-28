using NLog;
using System.Globalization;
using TestWork_DeliveryServices.BLL;
using TestWork_DeliveryServices.Interface;
using TestWork_DeliveryServices.Model;

namespace TestWork_DeliveryServices
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var logger = new Loggers();
            logger.Info("Приложение запущено");
            IParametrs parametrs = new StartParametr(args, logger);
            IInputData inputData = new InputData(logger);
            IFilterData filterData = new FilterData(logger);
            ISave save = new Save(logger);
            TestWorks testWorks = new TestWorks(parametrs, inputData, filterData, save, logger);
            await testWorks.TestWorkProcess();
        }
    }
}
