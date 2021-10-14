using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ChineseTime.Data
{
    internal class ChineseTimeDataLoader : IHostedService
    {
        private readonly int time = 5 * 60 ;
        private readonly string pathToData = Environment.CurrentDirectory + "/Data/Shicheng/";
        private readonly IChineseTimeDataManager shichengDataManager;

        public ChineseTimeDataLoader(IChineseTimeDataManager shichengDataManager)
        {
            this.shichengDataManager = shichengDataManager;
            LoadDataFromPath();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            LoadDataFromPath();
            await Task.Delay(this.time, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private bool LoadDataFromPath()
        {
            // Load all 12 chinese time
            for (var hour = 1; hour < 24; hour++)
            {
                if (hour % 2 == 0)
                {
                    continue;
                }

                // Read data file and convert to JSON
                var hourDataJSON = new ChineseTimeByHour();
                try
                {
                    var hourDataFileName = pathToData + this.LoadDataFromFile(hour);
                    using (StreamReader r = new StreamReader(hourDataFileName))
                    {
                        string json = r.ReadToEnd();
                        hourDataJSON = JsonConvert.DeserializeObject<ChineseTimeByHour>(json);
                    }
                }
                catch (Exception)
                {
                    // swallow. return false for logging.
                    Console.WriteLine($"Shichen: app failed to load data from hour {hour}.");
                }
                finally
                {
                    this.shichengDataManager.AddChineseTimeByHour(hour, new ChineseTimeByHour());
                    Console.WriteLine($"Shichen: app sucessfully load data from hour {hour}.");
                }
            }

            return true;
        }

        private string LoadDataFromFile(int hour)
        {
            return $"/{hour}.json";
        }
    }
}
