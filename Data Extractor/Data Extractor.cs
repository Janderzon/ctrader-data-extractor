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
        [Parameter(DefaultValue = 0.0)]
        public double Parameter { get; set; }

        [Parameter()]
        public string outputFilePath { get; set; }

        protected override void OnStart()
        {
            // Put your initialization logic here
        }

        protected override void OnTick()
        {
            File.AppendAllText(outputFilePath + ".csv", Ask.ToString() + ",\n");
        }

        protected override void OnStop()
        {
            // Put your deinitialization logic here
        }
    }
}
