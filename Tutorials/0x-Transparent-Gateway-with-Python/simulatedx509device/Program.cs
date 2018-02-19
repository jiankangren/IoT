using System;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

namespace simulatedx509device
{
    class Program
    {
        private static int MESSAGE_COUNT = 60;
        private static int SLEEP_TIME_IN_MILLISECONDS = 1000;
        private const int TEMPERATURE_THRESHOLD = 30;
        private static String deviceId = "devicewithx509cert";
        private static float temperature;
        private static float humidity;
        private static Random rnd = new Random();
        private static string absolutePathToDevicePfxFile = @"C:\dev\temp\devicewithx509cert.pfx";
        private static string iotHubConnection = "oscheeriothub.azure-devices.net";

        static async Task SendEvent(DeviceClient deviceClient)
        {
            string dataBuffer;
            Console.WriteLine("Device sending {0} messages to IoTHub...\n", MESSAGE_COUNT);

            try
            {
                for (int count = 0; count < MESSAGE_COUNT; count++)
                {
                    temperature = rnd.Next(20, 35);
                    humidity = rnd.Next(60, 80);
                    dataBuffer = string.Format("{{\"deviceId\":\"{0}\",\"messageId\":{1},\"temperature\":{2},\"humidity\":{3}}}", deviceId, count, temperature, humidity);
                    Message eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer));
                    eventMessage.Properties.Add("temperatureAlert", (temperature > TEMPERATURE_THRESHOLD) ? "true" : "false");
                    Console.WriteLine("\t{0}> Sending message: {1}, Data: [{2}]", DateTime.Now.ToLocalTime(), count, dataBuffer);

                    await deviceClient.SendEventAsync(eventMessage);
                    Thread.Sleep(SLEEP_TIME_IN_MILLISECONDS);
                }
            }
            catch(Exception exc)
            {
                ShowException(exc);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("### Simulated Device with x.509 certificate authorization.");
            try
            {
                
                var cert = new X509Certificate2(absolutePathToDevicePfxFile, "123");
                var auth = new DeviceAuthenticationWithX509Certificate(deviceId, cert);
                // var deviceClient = DeviceClient.Create(iotHubConnection, auth, TransportType.Amqp_Tcp_Only);
                var deviceClient = DeviceClient.Create(iotHubConnection, auth, TransportType.Mqtt);

                if (deviceClient == null)
                {
                    Console.WriteLine("Failed to create DeviceClient!");
                }
                else
                {
                    Console.WriteLine("Successfully created DeviceClient!");
                    SendEvent(deviceClient).Wait();
                }

                Console.WriteLine("Exiting...\n");
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private static void ShowException(Exception exc) 
        {
            Console.WriteLine("---------Exception-----------");
            Console.WriteLine($"exc: {exc.Message}");
            Console.WriteLine($"exc: {exc.StackTrace}");
            if (exc.InnerException != null) 
            {
                ShowException(exc.InnerException);
            }
        }

    }
}
