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
        public bool askPrice { get; set; }

        private string _filePath = string.Empty;

        protected override void OnStart()
        {
            _filePath = outputFilePath + outputFile;

            if (this.askPrice)
                File.AppendAllText(_filePath, "Ask,");

            File.AppendAllText(_filePath, "\n");
        }

        protected override void OnTick()
        {
            if (this.askPrice)
                File.AppendAllText(_filePath, Ask.ToString() + ",");

            File.AppendAllText(_filePath, "\n");
        }

        protected override void OnStop()
        {
            // Put your deinitialization logic here
        }
    }
}
