using HomeSensorApp.Models;
using Microsoft.Azure.Devices.Client;

namespace HomeSensorApp.Services
{
    public class IotHubDeviceService : NotifyPropertyBase
    {

        public IotHubDeviceService(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        #region Properties

        public string ConnectionString { get; set; }

        #endregion

        #region Methods

        public async void Open()
        {
            _deviceClient = DeviceClient.CreateFromConnectionString(ConnectionString);
            await _deviceClient.OpenAsync();
        }

        #endregion

        #region Internal Fields

        DeviceClient _deviceClient;

        #endregion
    }
}
