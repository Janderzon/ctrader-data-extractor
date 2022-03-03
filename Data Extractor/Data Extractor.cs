using System;
using System.IO;
using System.Linq;
using cAlgo.API;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;
using cAlgo.Indicators;

namespace cAlgo.Robots
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.FileSystem)]
    public class DataExtractor : Robot
    {
        [Parameter(DefaultValue = "History.csv")]
        public string outputFile { get; set; }

        [Parameter(DefaultValue = ".\\")]
        public string outputFilePath { get; set; }

        [Parameter(DefaultValue = true)]
        public bool openTime { get; set; }

        [Parameter(DefaultValue = true)]
        public bool openPrice { get; set; }

        [Parameter()]
        public DataSeries Source { get; set; }

        private string _filePath = string.Empty;

        protected override void OnStart()
        {
            _filePath = outputFilePath + outputFile;

            if (this.openTime)
                File.AppendAllText(_filePath, "OpenTime,");
            if (this.openPrice)
                File.AppendAllText(_filePath, "OpenPrice,");

            File.AppendAllText(_filePath, "\n");
        }

        protected override void OnBar()
        {
            if (this.openTime)
                File.AppendAllText(_filePath, Bars.Last().OpenTime.ToString() + ",");
            if (this.openPrice)
            {
                double openPriceSMANormalised = Bars.Last().Open - Indicators.SimpleMovingAverage(Source, 24 * 7).Result.LastValue;
                File.AppendAllText(_filePath, openPriceSMANormalised + ",");
            }

            File.AppendAllText(_filePath, "\n");
        }

        protected override void OnStop()
        {
            // Put your deinitialization logic here
        }
    }
}
