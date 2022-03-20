using System.IO;
using cAlgo.API;

namespace cAlgo.Robots
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.FileSystem)]
    public class DataExtractor : Robot
    {
        [Parameter(DefaultValue = "History.csv")]
        public string OutputFile { get; set; }

        [Parameter(DefaultValue = ".\\")]
        public string OutputFilePath { get; set; }

        [Parameter(DefaultValue = true)]
        public bool OpenTime { get; set; }

        [Parameter(DefaultValue = true)]
        public bool ClosePrice { get; set; }

        private string _filePath = string.Empty;

        protected override void OnStart()
        {
            _filePath = OutputFilePath + OutputFile;

            if (OpenTime)
                File.AppendAllText(_filePath, "OpenTime,");
            if (ClosePrice)
                File.AppendAllText(_filePath, "ClosePrice,");

            File.AppendAllText(_filePath, "\n");
        }

        protected override void OnBar()
        {
            if (OpenTime)
                File.AppendAllText(_filePath, Bars.Last(1).OpenTime.ToString() + ",");
            if (ClosePrice)
            {
                double closePriceSMANormalised = Bars.Last(1).Close - Indicators.SimpleMovingAverage(Bars.ClosePrices, 24 * 7).Result.Last(2);
                File.AppendAllText(_filePath, closePriceSMANormalised + ",");
            }

            File.AppendAllText(_filePath, "\n");
        }

        protected override void OnStop()
        {
            // Put your deinitialization logic here
        }
    }
}
