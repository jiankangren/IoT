using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;

namespace HomeSensorApp.Services
{
    public class IotHubService
    {
        public void SetConnectionString(string connectionString)
        {
            //_connectionString = connectionString;
            //deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey), TransportType.Mqtt);
            _deviceClient = DeviceClient.CreateFromConnectionString(connectionString);

            //SendDeviceToCloudMessagesAsync();
            //Console.ReadLine();
        }

        DeviceClient _deviceClient;
        //string iotHubUri = "{iot hub hostname}";
        //string deviceKey = "{device key}";

        public async void SendDeviceToCloud(string sensorHubName, string sensorName, object sensorValue)
        {
                //double currentTemperature = minTemperature + rand.NextDouble() * 15;
                //double currentHumidity = minHumidity + rand.NextDouble() * 20;

                var telemetryDataPoint = new
                {
                    sensorHubName = sensorHubName,
                    sensorName = sensorName,
                    sensorValue = sensorValue
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                //message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                await _deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

        }

       
    }
}
