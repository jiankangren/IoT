using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace CreateDeviceIdentity
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Create Device Identity");
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        static RegistryManager registryManager;
        // static string connectionString = "{iot hub connection string}";
        static string connectionString = "HostName=oscheeriothub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=k/BjpZ7WxfVwklj3f74o+ad1kvR/0qnJKPhyyBGgKdw=";

        private static async Task AddDeviceAsync()
        {
            string deviceId = "myFirstDevice";
            Device device;
            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
