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
        public string OutputFile { get; set; }

        [Parameter(DefaultValue = ".\\")]
        public string OutputFilePath { get; set; }

        [Parameter(DefaultValue = true)]
        public bool OpenTime { get; set; }

        [Parameter(DefaultValue = true)]
        public bool OpenPrice { get; set; }

        [Parameter()]
        public DataSeries Source { get; set; }

        private string _filePath = string.Empty;

        protected override void OnStart()
        {
            _filePath = OutputFilePath + OutputFile;

            if (this.OpenTime)
                File.AppendAllText(_filePath, "OpenTime,");
            if (this.OpenPrice)
                File.AppendAllText(_filePath, "OpenPrice,");

            File.AppendAllText(_filePath, "\n");
        }

        protected override void OnBar()
        {
            if (this.OpenTime)
                File.AppendAllText(_filePath, Bars.Last().OpenTime.ToString() + ",");
            if (this.OpenPrice)
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
