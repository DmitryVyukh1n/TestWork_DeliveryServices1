using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWork_DeliveryServices.Interface;
using TestWork_DeliveryServices.Model;

namespace TestWork_DeliveryServices.BLL
{
    internal class InputData:IInputData
    {
        private string _pathFile;
        private readonly Loggers _logger;
        public InputData(Loggers logger)
        {
            _logger = logger;
        }

        public async Task<List<InputModel>> GetInputData(StartParametrs parametr)
        {
            _pathFile=parametr.PathFile;
            _logger.Info("Запрос данных файла");
            var file = await GetFile();
            if (file == null)
            {
                return null;
            }
            _logger.Info("Данные файла получены");
            _logger.Info("Запуск парсинга файла");
            var inputModel = await ParserFile(file);
            _logger.Info("Данные получены");
            return inputModel;
        }

        private async Task<string> GetFile()
        {
            if (!File.Exists(_pathFile))
            {
                _logger.Fatal("Отсутсвует файл по указанному пути!");
                return null;
                
            }
            return await File.ReadAllTextAsync(_pathFile);
        }

        private async Task<List<InputModel>> ParserFile(string file)
        {
            List<InputModel> inputModels = new List<InputModel>();
            _logger.Info("Разделяем файл построчно");
            var rows = file.Split("\n");
            int count =0;
            foreach (var row in rows)
            {
                _logger.Info("Добавление полученного объекта файла в список");
                var result = await GetInput(row);
                if (result == null)
                {
                    _logger.Info($"Данных в строке = {count} не обнаружено!");
                }
                else
                {
                    inputModels.Add(result);
                }
                count++;
            }
            _logger.Info("Возврращаем список объектов");
            return inputModels;
        }

        private async Task<InputModel> GetInput(string row)
        {
            InputModel inputModel = new InputModel();
            _logger.Info("Разделение строки");
            var items = row.Split(";", StringSplitOptions.RemoveEmptyEntries);
            if (items.Count() == 0)
            {
                return null;
            }
            _logger.Info("Заполнение объекта");
            inputModel.OrderNumber = items[0];
            inputModel.OrderWeight = Convert.ToInt32(items[1]);
            inputModel.OrderRegion =items[2];
            inputModel.OrderDeliveryTime = Convert.ToDateTime(items[3]);

            _logger.Info("Возвращаем заполненный объект");
            return inputModel;
        }
    }
}
