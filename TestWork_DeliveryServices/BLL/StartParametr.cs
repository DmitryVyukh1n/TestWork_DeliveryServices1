using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork_DeliveryServices.Interface;
using TestWork_DeliveryServices.Model;

namespace TestWork_DeliveryServices.BLL
{
    internal class StartParametr:IParametrs
    {
        private readonly string[] _args;
        private readonly Loggers _loggers;

        public StartParametr(string[] args, Loggers loggers)
        {
            _args = args;
            _loggers = loggers;
        }

        public async Task<StartParametrs> GetStartParametrs()
        {
            bool statusEnter = false;
            StartParametrs param = null;
            while (!statusEnter)
            {

                param = await GetArgs();
                if (param == null)
                {
                    param = await GetArgsParam();
                    if (param == null)
                    {
                        param = await GetArgsEnter();
                        if (param == null)
                            statusEnter = false;
                    }
                }
                if (param != null)
                {
                    statusEnter = true;
                    return param;
                }
               
                _loggers.Info("Параметры поиска введены не верно!");
                Console.Clear();
                Console.WriteLine("Дааные введены не верно! Введите их еще раз!");
            }
            return param;
        }

        private async Task<StartParametrs> GetArgs()
        {
            if (_args.Length < 4)
            {
                string message = _args.Length == 0 ? "Аргументы не указаны!" : "Указаны не все параметры в аргументах!";
                Console.WriteLine(message);
                _loggers.Error(message);
                return null;
            }

            string pathFile = _args[0];
            string cityDistinct = _args[1];
            var firstDeliveryDateTime = await GetDateTime(_args[2]);
            string deliveryOrder = _args[3];

            return await CheckDataParametrs(pathFile, cityDistinct, firstDeliveryDateTime, deliveryOrder);
        }

        private async Task<StartParametrs> GetArgsParam()
        {
            string pathFile = Helper.GetConst("pathFile");
            string cityDistinct = Helper.GetConst("_cityDistinct");
            var firstDeliveryDateTime = await GetDateTime(Helper.GetConst("_firstDeliveryDateTime"));
            string deliveryOrder = Helper.GetConst("_deliveryOrder");
            return await CheckDataParametrs(pathFile, cityDistinct, firstDeliveryDateTime, deliveryOrder);

        }

        private async Task<StartParametrs> GetArgsEnter()
        {
            string pathFile = await PromptUserForInput("Введите путь до файла: ");
            string cityDistinct = await PromptUserForInput("Введите Район доставки: ");
            var firstDeliveryDateTime = await GetDateTime(await PromptUserForInput("Время первой доставки: "));
            string deliveryOrder = await PromptUserForInput("Введите путь для файла с результатами: ");
            return await CheckDataParametrs(pathFile, cityDistinct, firstDeliveryDateTime, deliveryOrder);
        }
        private  async Task<string> PromptUserForInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }



        private async Task<StartParametrs> CheckDataParametrs(string pathFile, string cityDistinct, DateTime? firstDeliveryDateTime, string deliveryOrder)
        {
            if (string.IsNullOrEmpty(pathFile) ||
                    string.IsNullOrEmpty(cityDistinct) ||
                    firstDeliveryDateTime == null ||
                    string.IsNullOrEmpty(deliveryOrder))
            {
                return null;
            }


            return new StartParametrs
            {
                PathFile = pathFile,
                CityDistinct = cityDistinct,
                FirstDeliveryDateTime = firstDeliveryDateTime,
                DeliveryOrder = deliveryOrder
            };
        }

        public async Task<DateTime?> GetDateTime(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return null;

            DateTime parsedDate;
            string[] formats = { "dd-MM-yyyy HH:mm:ss", "yyyy-MM-dd HH:mm:ss", "MM/dd/yyyy HH:mm:ss" }; 

            if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else
            {
                Console.WriteLine("Дата введена с ошибками!");
                _loggers.Error("Параметр дата введена не верно!");
                return null;
            }
        }
    }
}
