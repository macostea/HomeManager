using System;
using System.Text;
using HomeManager.Common.Repository;
using HomeManager.Common.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Function
{
    public class FunctionHandler
    {
        public async Task<string> Handle(string input) {
            var inputDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);

            var dBContext = new TimescaleDBContext();
            var repository = new TempSensorReadingsRepository(dBContext);


            switch (inputDictionary["type"])
            {
                case "temperature":
                    var reading = JsonConvert.DeserializeObject<SensorReading<double>>(inputDictionary["reading"].ToString());
                    var location = inputDictionary["location"];
                    var sensor = new TempSensor(location.ToString(), repository);

                    return await sensor.SaveReading(reading) ? "worked" : "didn't work";
            }
            return "didn't work";
        }
    }
}
