using HomeSensorApp.Services;
using Windows.Storage;

namespace HomeSensorApp.Models
{
    public class ApplicationSettings : NotifyPropertyBase
    {
        public ApplicationSettings()
        {
            // If not design time, try to load saved settings

            _localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            _iotHub = new IotHubService();
            UpdateConnection();
        }

        ApplicationDataContainer _localSettings;
        IotHubService _iotHub;

        private void UpdateConnection()
        {
            _iotHub.SetConnectionString(DeviceConnectionString);
        }

        #region Strings

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