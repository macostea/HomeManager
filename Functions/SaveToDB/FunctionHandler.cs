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
        public async Task<string> Handle(string input, string connString) {
           var inputDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(input);

            var dBContext = new TimescaleDBContext(connString);
            var jsonSensor = inputDictionary["sensor"];
            var jsonReading = inputDictionary["reading"];

            var sensor = new Sensor()
            {
                Id = Convert.ToInt32(jsonSensor["id"]),
                Type = (string)jsonSensor["type"]
            };

            switch (sensor.Type)
            {
                case "temperature":
                    var repository = new TempSensorReadingsRepository(dBContext, sensor);
                    var time = (DateTime)jsonReading["time"];
                    var reading = (double)jsonReading["reading"];
                    var sensorReading = new SensorReading<double>(time, reading);

                    return await repository.Add(sensorReading) ? "worked" : "didn't work";
            }
            return "didn't work";
        }
    }
}
