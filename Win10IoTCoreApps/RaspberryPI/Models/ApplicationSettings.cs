using HomeSensorApp.Services;
using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI.Core;

namespace HomeSensorApp.Models
{
    public class ApplicationSettings : NotifyPropertyBase
    {

        public ApplicationSettings()
        {
            // Must be first command for some reasons
            
            //Task.Run(async () =>
            //{
            //    try
            //    {

            //        ISenseHat senseHat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);

            //        while (1 == 1)
            //        {

            //            senseHat.Sensors.HumiditySensor.Update();
            //            var temp = senseHat.Sensors.Temperature;
            //            Debug.WriteLine($"Temp: {temp}");
            //            //UpdateSensor(temp);
            //            //Sleep(new TimeSpan(0, 0, 1));
            //        }
            //    }
            //    catch (Exception exc)
            //    {
            //        Debug.WriteLine(exc.Message);
            //    }


            //}).ConfigureAwait(false);


            // If not design time, try to load saved settings

            _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            _iotHub = new IotHubService();
            UpdateConnection();

            //_sensorService = new SensorService();
            //_sensorService.CreateSensors();
        }

        ApplicationDataContainer _localSettings;
        IotHubService _iotHub;
        //SensorService _sensorService;

        private void UpdateConnection()
        {
            _iotHub.SetConnectionString(DeviceConnectionString);
        }

        #region Dynamic Values

        private ObservableCollection<BaseSensorHub> _sensorHubs = new ObservableCollection<BaseSensorHub>();
        public ObservableCollection<BaseSensorHub> SensorHubs { get => _sensorHubs; set => _sensorHubs = value; }

        public async void AddSensorHub(BaseSensorHub sensorHub)
        {
            foreach(var sensor in sensorHub.Sensors)
            {
                sensor.SensorValuesUpdated += Sensor_SensorValuesUpdated;
            }

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    _sensorHubs.Add(sensorHub);
                    //OnPropertyChanged("SensorHubs");
                    //_output.Text = text;
                });

            
        }

        private void Sensor_SensorValuesUpdated(object sender, SensorValuesUpdatedEventArgs e)
        {
            string message = $"{e.SensorHubName}.{e.SensorName} = {e.SensorValue}";
            StatusMessage = message + Environment.NewLine + StatusMessage;

            // Send Message to IoT Hub
            _iotHub.SendDeviceToCloud(e.SensorHubName, e.SensorName, e.SensorValue);
        }

        #endregion

        #region Static values

        public string ApplicationTitle
        {
            get { return "Home Sensors"; }
            set { }
        }

        #endregion

        #region User Settings

        public string DeviceConnectionString
        {
            get
            {
                string result = $"HostName={_hostName};DeviceId={_deviceId};SharedAccessKey={_sharedAccessKey}";
                return result;
            }
        }
        
        // default values
        private string _deviceId = "winpi001";
        private string _hostName = "oscheeriothub.azure-devices.net";
        private string _sharedAccessKey = "V05R254hEkusyL6ud27HVgeQpzAoAmNIXQRtDUBDAaI=";
        private int _updateInterval = 5000;

        public int UpdateInterval
        {
            get
            {
                return GetValueAsInt("UpdateInterval", _updateInterval);
            }
            set
            {
                SetValue("UpdateInterval", value);
            }
        }

        public string DeviceId
        {
            get
            {
                return GetValueAsString("DeviceId", _deviceId);
            }
            set
            {
                SetValue("DeviceId", value);
                UpdateConnection();
            }
        }

        public string HostName
        {
            get
            {
                return GetValueAsString("HostName", _hostName);
            }
            set
            {
                SetValue("HostName", value);
                UpdateConnection();
            }
        }

        public string SharedAccessKey
        {
            get
            {
                return GetValueAsString("SharedAccessKey", _sharedAccessKey);
            }
            set
            {
                SetValue("SharedAccessKey", value);
                UpdateConnection();
            }
        }

        public string Location { get; set; }

        private string _statusMessage;
        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value; OnPropertyChanged();
            }
        }

        #endregion

        #region Internal Settings 

        private void SetValue(string propertyKey, object propertyValue)
        {
            _localSettings.Values[propertyKey] = propertyValue;
        }

        private object GetValue(string propertyKey, object defaultValue = null)
        {
            if (_localSettings.Values.ContainsKey(propertyKey))
            {
                return _localSettings.Values[propertyKey];
            }
            else
            {
                return defaultValue;
            }
        }

        private string GetValueAsString(string propertyKey, object defaultValue = null)
        {
            return (string)GetValue(propertyKey, (string)defaultValue);
        }

        private int GetValueAsInt(string propertyKey, object defaultValue = null)
        {
            return (int)GetValue(propertyKey, (int)defaultValue);
        }

        #endregion
    }
}