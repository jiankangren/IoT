using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using HomeSensorApp.Models;

namespace HomeSensorApp.Services
{
    public class IotHubService
    {
        public IotHubService()
        {

        }

        internal void SetSettings(SettingsService appSettings)
        {
            SetConnectionString(appSettings.DeviceConnectionString);
        }

        private void SetConnectionString(string connectionString)
        {
            _deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
        }

        DeviceClient _deviceClient;

        public async void SendDeviceToCloud(SensorValuePackage sensorValue)
        {
            var telemetryDataPoint = new
            {
                sensorHubName = sensorValue.SensorHubName,
                sensorName = sensorValue.SensorName,
                sensorValue = sensorValue.SensorValue
            };
            var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));
            //message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

            await _deviceClient.SendEventAsync(message);
            Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
        }

        public void SendStatusMessage(string source, string message)
        {
            SensorValuePackage data = new SensorValuePackage();
            data.SensorHubName = "App";
            data.SensorName = source;
            data.SensorValue = message;

            SendDeviceToCloud(data);
        }

    }
}
