using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork_DeliveryServices.Interface;
using TestWork_DeliveryServices.Model;

namespace TestWork_DeliveryServices.BLL
{
    internal class Save:ISave
    {
        private readonly Loggers _logger;

        public Save(Loggers logger)
        {
            _logger = logger; 
        }

        public async Task Saves(StartParametrs parametr, List<InputModel> inputModels)
        {
            using (StreamWriter writer = new StreamWriter(parametr.DeliveryOrder, append: true))
            {
                foreach (var order in inputModels)
                {
                    string orderInfo = $"Номер заказа: {order.OrderNumber}, Вес: {order.OrderWeight}, Регион: {order.OrderRegion}, Дата доставки: {order.OrderDeliveryTime}";
                    Console.WriteLine(orderInfo);
                    await writer.WriteLineAsync(orderInfo);  
                }
            }
        }
    }
}
